using CatBreedsDetector.Classification;
using Microsoft.ML;

namespace TestApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var classifier = new CatBreedClassifier();

            ITransformer model = classifier.GenerateModel();

            classifier.ClassifySingleImage(model);
        }
    }
}
