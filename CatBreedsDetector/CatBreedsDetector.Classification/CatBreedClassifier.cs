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

        public void ClassifySingleImage()
        {
            var imageData = new ImageData()
            {
                ImagePath = Constants._predictSingleImage
            };

            var model = this.GenerateModel();

            var predictor = this.mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);
            var prediction = predictor.Predict(imageData);

            Console.WriteLine($"Image: {Path.GetFileName(imageData.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()} ");
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
