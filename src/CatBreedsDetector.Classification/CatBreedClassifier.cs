namespace CatBreedsDetector.Classification
{
    using System;
    using System.IO;
    using CatBreedsDetector.Classification.Common;
    using CatBreedsDetector.Classification.Interfaces;
    using CatBreedsDetector.Classification.Models;
    using Microsoft.ML;
    using CommonConstants = CatBreedsDetector.Common;

    /// <summary>
    /// A custom implementation of the <see cref="ICatBreedClassifier"/> interface.
    /// </summary>
    public class CatBreedClassifier : ICatBreedClassifier
    {
        private readonly MLContext mlContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatBreedClassifier"/> class.
        /// </summary>
        public CatBreedClassifier()
        {
            this.mlContext = new MLContext();
        }

        /// <inheritdoc />
        public ImagePrediction ClassifySingleImage(string path)
        {
            // TODO: This method needs refactoring. We should accept the prediction engine from outside.
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                throw new ArgumentException(CommonConstants.Constants.Message.MissingOrInvalidFile);

            var imageData = new ImageData()
            {
                ImagePath = path,
            };

            var model = this.GenerateModel();

            var predictor = this.mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);

            return predictor.Predict(imageData);
        }

        private ITransformer GenerateModel()
        {
            if (File.Exists(Constants.SavedModelFileName))
            {
                // TODO: Should be fixed to check whether the model is loaded.
                var trainedModel = this.mlContext.Model.Load(Constants.SavedModelFileName, out var modelSchema);

                return trainedModel;
            }

            var pipeline = this.mlContext.Transforms.LoadImages(
                    outputColumnName: "input",
                    imageFolder: Constants.ImagesFolder,
                    inputColumnName: nameof(ImageData.ImagePath))
                .Append(this.mlContext.Transforms.ResizeImages(
                    outputColumnName: "input",
                    imageWidth: InceptionSettings.ImageWidth,
                    imageHeight: InceptionSettings.ImageHeight,
                    inputColumnName: "input"))
                .Append(this.mlContext.Transforms.ExtractPixels(
                    outputColumnName: "input",
                    interleavePixelColors: InceptionSettings.ChannelsLast,
                    offsetImage: InceptionSettings.Mean))
                .Append(this.mlContext.Model.LoadTensorFlowModel(Constants.InceptionTensorFlowModel)
                    .ScoreTensorFlowModel(
                    outputColumnNames: new[] { "softmax2_pre_activation" },
                    inputColumnNames: new[] { "input" },
                    addBatchDimensionInput: true))
                .Append(this.mlContext.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "LabelKey",
                    inputColumnName: "Label"))
                .Append(this.mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(
                    labelColumnName: "LabelKey",
                    featureColumnName: "softmax2_pre_activation"))
                .Append(this.mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
                .AppendCacheCheckpoint(this.mlContext);

            var trainingData = this.mlContext.Data.LoadFromTextFile<ImageData>(path: Constants.TrainTagsTsv);

            var model = pipeline.Fit(trainingData);

            this.mlContext.Model.Save(model, trainingData.Schema, Constants.SavedModelFileName);

            return model;
        }
    }
}
