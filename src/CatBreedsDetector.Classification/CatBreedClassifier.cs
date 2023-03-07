namespace CatBreedsDetector.Classification
{
    using System;
    using System.IO;
    using CatBreedsDetector.Classification.Common;
    using CatBreedsDetector.Classification.Interfaces;
    using CatBreedsDetector.Classification.Models;
    using CatBreedsDetector.Common.Execution;
    using Microsoft.ML;
    using CommonConstants = CatBreedsDetector.Common;

    /// <summary>
    /// A custom implementation of the <see cref="ICatBreedClassifier"/> interface.
    /// </summary>
    public class CatBreedClassifier : ICatBreedClassifier
    {
        private readonly MLContext _mlContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatBreedClassifier"/> class.
        /// </summary>
        public CatBreedClassifier()
        {
            this._mlContext = new MLContext();
        }

        /// <inheritdoc />
        public ExecutionResult<ImagePrediction> ClassifySingleImage(string path)
        {
            // TODO: This method needs refactoring. We should accept the prediction engine from outside.
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                throw new ArgumentException(path);

            var imageData = new ImageData()
            {
                ImagePath = path,
            };

            var model = this.GenerateModel();

            var predictor = this._mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);

            return ExecutionResult<ImagePrediction>.SuccessWith(predictor.Predict(imageData));
        }

        private ITransformer GenerateModel()
        {
            if (File.Exists(Constants.SavedModelFileName))
            {
                // TODO: Should be fixed to check whether the model is loaded.
                var trainedModel = this._mlContext.Model.Load(Constants.SavedModelFileName, out var modelSchema);

                return trainedModel;
            }

            var pipeline = this._mlContext.Transforms.LoadImages(
                    outputColumnName: "input",
                    imageFolder: Constants.ImagesFolder,
                    inputColumnName: nameof(ImageData.ImagePath))
                .Append(this._mlContext.Transforms.ResizeImages(
                    outputColumnName: "input",
                    imageWidth: InceptionSettings.ImageWidth,
                    imageHeight: InceptionSettings.ImageHeight,
                    inputColumnName: "input"))
                .Append(this._mlContext.Transforms.ExtractPixels(
                    outputColumnName: "input",
                    interleavePixelColors: InceptionSettings.ChannelsLast,
                    offsetImage: InceptionSettings.Mean))
                .Append(this._mlContext.Model.LoadTensorFlowModel(Constants.InceptionTensorFlowModel)
                    .ScoreTensorFlowModel(
                    outputColumnNames: new[] { "softmax2_pre_activation" },
                    inputColumnNames: new[] { "input" },
                    addBatchDimensionInput: true))
                .Append(this._mlContext.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "LabelKey",
                    inputColumnName: "Label"))
                .Append(this._mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(
                    labelColumnName: "LabelKey",
                    featureColumnName: "softmax2_pre_activation"))
                .Append(this._mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
                .AppendCacheCheckpoint(this._mlContext);

            var trainingData = this._mlContext.Data.LoadFromTextFile<ImageData>(path: Constants.TrainTagsTsv);

            var model = pipeline.Fit(trainingData);

            this._mlContext.Model.Save(model, trainingData.Schema, Constants.SavedModelFileName);

            return model;
        }
    }
}
