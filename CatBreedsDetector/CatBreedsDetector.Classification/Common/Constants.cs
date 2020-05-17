using System.Reflection;

namespace CatBreedsDetector.Classification.Common
{
    using System;
    using System.IO;

    public class Constants
    {
        public static readonly string _assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assets");
        public static readonly string _imagesFolder = Path.Combine(_assetsPath, "images");
        public static readonly string _trainTagsTsv = Path.Combine(_imagesFolder, "tags.tsv");
        public static readonly string _testTagsTsv = Path.Combine(_imagesFolder, "test-tags.tsv");
        public static readonly string _predictSingleImage = Path.Combine(_imagesFolder, "abyssinian-cat-4.jpg");
        public static readonly string _inceptionTensorFlowModel = Path.Combine(_assetsPath, "inception", "tensorflow_inception_graph.pb");
    }
}
