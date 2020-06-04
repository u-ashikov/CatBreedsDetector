namespace CatBreedsDetector.Web.Models
{
    public class CatBreedPredictionResultModel
    {
        public CatBreedPredictionResultModel(string breed, double predictionProbability)
        {
            this.Breed = breed;
            this.PredictionProbability = predictionProbability;
        }

        public string Breed { get; }

        public double PredictionProbability { get; }
    }
}
