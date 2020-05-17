namespace CatBreedsDetector.Classification
{
    using Common;
    using Interfaces;
    using Microsoft.ML;
    using Models;
    using System;
    using System.IO;
    using System.Linq;

    public class CatBreedClassifier : ICatBreedClassifier
    {
        private readonly MLContext mlContext;

        public CatBreedClassifier()
        {
            this.mlContext = new MLContext();
        }

        public string ClassifySingleImage(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                throw new ArgumentException("File is missing or invalid!");
            }

            // TODO: Train and save the model and use it.

            var imageData = new ImageData()
            {
                ImagePath = path
            };

            var model = this.GenerateModel();

            var predictor = this.mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);
            var prediction = predictor.Predict(imageData);

            return prediction.PredictedLabelValue;
        }

        private ITransformer GenerateModel()
        {
            var pipeline = this.mlContext.Transforms.LoadImages(outputColumnName: "input",
                    imageFolder: Constants._imagesFolder, inputColumnName: nameof(ImageData.ImagePath))
                .Append(mlContext.Transforms.ResizeImages(outputColumnName: "input",
                    imageWidth: InceptionSettings.ImageWidth, imageHeight: InceptionSettings.ImageHeight,
                    inputColumnName: "input"))
                .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "input",
                    interleavePixelColors: InceptionSettings.ChannelsLast, offsetImage: InceptionSettings.Mean))
                .Append(mlContext.Model.LoadTensorFlowModel(Constants._inceptionTensorFlowModel)
                    .ScoreTensorFlowModel(outputColumnNames: new[] {"softmax2_pre_activation"},
                        inputColumnNames: new[] {"input"}, addBatchDimensionInput: true))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey",
                    inputColumnName: "Label"))
                .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "LabelKey",
                    featureColumnName: "softmax2_pre_activation"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
                .AppendCacheCheckpoint(this.mlContext);

            var trainingData = this.mlContext.Data.LoadFromTextFile<ImageData>(path: Constants._trainTagsTsv);

            var model = pipeline.Fit(trainingData);

            return model;
        }
    }
}
