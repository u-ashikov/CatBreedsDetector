namespace CatBreedsDetector.Tests.Common.Mocks
{
    using CatBreedsDetector.Classification.Interfaces;
    using CatBreedsDetector.Classification.Models;
    using CatBreedsDetector.Tests.Common.Helpers;
    
    public class CatBreedClassifierMock : ICatBreedClassifier
    {
        /// <inheritdoc />
        public ImagePrediction ClassifySingleImage(string path) => new ImagePrediction()
        {
            PredictedLabelValue = TestsHelper.GenerateRandomString(),
            Score = new float[] { (float)TestsHelper.GenerateRandomProbability()}
        };
    }
}