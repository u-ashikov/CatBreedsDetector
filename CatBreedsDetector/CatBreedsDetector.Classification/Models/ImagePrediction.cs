namespace CatBreedsDetector.Classification.Models
{
    using System;
    using System.Linq;

    public class ImagePrediction : ImageData
    {
        public float[] Score { get; set; }

        public string PredictedLabelValue { get; set; }

        public double PredictionProbability => this.Score[this.Score.AsSpan().IndexOf(this.Score.Max())];
    }
}
