namespace CatBreedsDetector.Models.ViewModels;

/// <summary>
/// A class representing a cat breed prediction result model.
/// </summary>
public class CatBreedPredictionResultModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CatBreedPredictionResultModel"/> class.
    /// </summary>
    /// <param name="breed">The breed of the cat.</param>
    /// <param name="predictionProbability">The prediction probability of the operation.</param>
    public CatBreedPredictionResultModel(string breed, double predictionProbability)
    {
        this.Breed = breed;
        this.PredictionProbability = predictionProbability;
    }

    /// <summary>
    /// Gets the detected cat's breed.
    /// </summary>
    public string Breed { get; }

    /// <summary>
    /// Gets the prediction probability value.
    /// </summary>
    public double PredictionProbability { get; }
}