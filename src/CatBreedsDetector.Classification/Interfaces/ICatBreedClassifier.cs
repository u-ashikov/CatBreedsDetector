namespace CatBreedsDetector.Classification.Interfaces;

using CatBreedsDetector.Classification.Models;
using CatBreedsDetector.Common.Execution;

/// <summary>
/// An interface defining the structure of a component responsible for classifying a given image.
/// </summary>
public interface ICatBreedClassifier
{
    /// <summary>
    /// Use this method to classify a given image of a cat.
    /// </summary>
    /// <param name="path">The path to the image that should be classified.</param>
    /// <returns>An <see cref="ExecutionResult{T}"/> containing the information of the classification process.</returns>
    ExecutionResult<ImagePrediction> ClassifySingleImage(string path);
}