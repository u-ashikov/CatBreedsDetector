namespace CatBreedsDetector.Classification.Models
{
    using System;
    using System.Linq;

    /// <summary>
    /// A class representing a model containing information of the prediction result.
    /// </summary>
    public class ImagePrediction : ImageData
    {
        /// <summary>
        /// Gets the score of the prediction as a floating point number.
        /// </summary>
        public float[] Score { get; init; }

        /// <summary>
        /// Gets or sets a predicted label value.
        /// </summary>
        public string PredictedLabelValue { get; set; }

        /// <summary>
        /// Gets a value representing the prediction probability.
        /// </summary>
        public double PredictionProbability => this.Score[this.Score.AsSpan().IndexOf(this.Score.Max())];
    }
}
