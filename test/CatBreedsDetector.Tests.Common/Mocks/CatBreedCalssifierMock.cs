namespace CatBreedsDetector.Tests.Common.Mocks;

using CatBreedsDetector.Classification.Interfaces;
using CatBreedsDetector.Classification.Models;
using CatBreedsDetector.Common.Execution;
using CatBreedsDetector.Tests.Common.Helpers;

public class CatBreedClassifierMock : ICatBreedClassifier
{
    /// <inheritdoc />
    public ExecutionResult<ImagePrediction> ClassifySingleImage(string path)
    {
        var imagePrediction = new ImagePrediction()
        {
            PredictedLabelValue = TestsHelper.GenerateRandomString(),
            Score = new[] {(float) TestsHelper.GenerateRandomProbability()}
        };

        return ExecutionResult<ImagePrediction>.SuccessWith(imagePrediction);
    }
}