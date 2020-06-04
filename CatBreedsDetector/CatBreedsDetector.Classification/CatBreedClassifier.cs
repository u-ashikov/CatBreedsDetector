namespace CatBreedsDetector.Classification
{
    using Common;
    using Interfaces;
    using Microsoft.ML;
    using Models;
    using System;
    using System.IO;
    using System.Linq;
    using CommonConstants = CatBreedsDetector.Common;

    public class CatBreedClassifier : ICatBreedClassifier
    {
        private readonly MLContext mlContext;

        public CatBreedClassifier()
        {
            this.mlContext = new MLContext();
        }

        public ImagePrediction ClassifySingleImage(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                throw new ArgumentException(CommonConstants.Constants.Message.MissingOrInvalidFile);
            }

            var imageData = new ImageData()
            {
                ImagePath = path
            };

            var model = this.GenerateModel();

            var predictor = this.mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);

            return predictor.Predict(imageData);
        }

        private ITransformer GenerateModel()
        {
            if (File.Exists(Constants.SavedModelFileName))
            {
                var trainedModel = mlContext.Model.Load(Constants.SavedModelFileName, out var modelSchema);

                return trainedModel;
            }

            var pipeline = this.mlContext.Transforms.LoadImages(outputColumnName: "input",
                    imageFolder: Constants.ImagesFolder, inputColumnName: nameof(ImageData.ImagePath))
                .Append(mlContext.Transforms.ResizeImages(outputColumnName: "input",
                    imageWidth: InceptionSettings.ImageWidth, imageHeight: InceptionSettings.ImageHeight,
                    inputColumnName: "input"))
                .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "input",
                    interleavePixelColors: InceptionSettings.ChannelsLast, offsetImage: InceptionSettings.Mean))
                .Append(mlContext.Model.LoadTensorFlowModel(Constants.InceptionTensorFlowModel)
                    .ScoreTensorFlowModel(outputColumnNames: new[] {"softmax2_pre_activation"},
                        inputColumnNames: new[] {"input"}, addBatchDimensionInput: true))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey",
                    inputColumnName: "Label"))
                .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "LabelKey",
                    featureColumnName: "softmax2_pre_activation"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
                .AppendCacheCheckpoint(this.mlContext);

            var trainingData = this.mlContext.Data.LoadFromTextFile<ImageData>(path: Constants.TrainTagsTsv);

            var model = pipeline.Fit(trainingData);

            mlContext.Model.Save(model, trainingData.Schema, Constants.SavedModelFileName);

            return model;
        }
    }
}
