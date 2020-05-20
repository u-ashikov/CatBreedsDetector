namespace CatBreedsDetector.Classification.Common
{
    using System.IO;
    using System.Reflection;

    public class Constants
    {
        public static readonly string AssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assets");
        public static readonly string ImagesFolder = Path.Combine(AssetsPath, "images");
        public static readonly string TrainTagsTsv = Path.Combine(ImagesFolder, "tags.tsv");
        public static readonly string InceptionTensorFlowModel = Path.Combine(AssetsPath, "inception", "tensorflow_inception_graph.pb");
        public static readonly string SavedModelFileName = "model.zip";
    }
}
