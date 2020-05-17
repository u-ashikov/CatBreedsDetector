namespace CatBreedsDetector.Classification.Interfaces
{
    public interface ICatBreedClassifier
    {
        string ClassifySingleImage(string path);
    }
}
