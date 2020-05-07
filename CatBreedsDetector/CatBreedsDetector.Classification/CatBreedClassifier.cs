namespace CatBreedsDetector.Classification
{
    using Common;
    using Microsoft.ML;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CatBreedClassifier
    {
        private readonly MLContext mlContext;

        public CatBreedClassifier()
        {
            this.mlContext = new MLContext();
        }

        public void DisplayResults(IEnumerable<ImagePrediction> imagePredictionData)
        {
            foreach (var imagePrediction in imagePredictionData)
            {
                Console.WriteLine($"Image: {Path.GetFileName(imagePrediction.ImagePath)} predicted as: {imagePrediction.PredictedLabelValue} with score: {imagePrediction.Score.Max()} ");
            }
        }

        public IEnumerable<ImageData> ReadFromTsv(string file, string folder)
        {
            return File.ReadAllLines(file)
                .Select(line => line.Split('\t'))
                .Select(line => new ImageData()
                {
                    ImagePath = Path.Combine(folder, line[0])
                });
        }

        public void ClassifySingleImage(ITransformer model)
        {
            var imageData = new ImageData()
            {
                ImagePath = Constants._predictSingleImage
            };

            var predictor = this.mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);
            var prediction = predictor.Predict(imageData);

            Console.WriteLine($"Image: {Path.GetFileName(imageData.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()} ");
        }

        public ITransformer GenerateModel()
        {
            var pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input",
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
                .AppendCacheCheckpoint(mlContext);

            IDataView trainingData = mlContext.Data.LoadFromTextFile<ImageData>(path: Constants._trainTagsTsv, hasHeader: false);

            ITransformer model = pipeline.Fit(trainingData);

            return model;
        }
    }
}
