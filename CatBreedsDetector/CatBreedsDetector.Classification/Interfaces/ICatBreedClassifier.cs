namespace CatBreedsDetector.Classification.Interfaces
{
    using Models;

    public interface ICatBreedClassifier
    {
        ImagePrediction ClassifySingleImage(string path);
    }
}
