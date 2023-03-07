namespace CatBreedsDetector.Classification.Common
{
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// A class containing common constants for the application.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Gets the name of the saved trained model as a zip archive.
        /// </summary>
        public const string SavedModelFileName = "model.zip";

        /// <summary>
        /// Gets the images folder path.
        /// </summary>
        public static readonly string ImagesFolder = Path.Combine(AssetsPath, "images");

        /// <summary>
        /// Gets the training tags path.
        /// </summary>
        public static readonly string TrainTagsTsv = Path.Combine(ImagesFolder, "tags.tsv");

        /// <summary>
        /// Gets the tensor flow model path.
        /// </summary>
        public static readonly string InceptionTensorFlowModel = Path.Combine(AssetsPath, "inception", "tensorflow_inception_graph.pb");

        /// <summary>
        /// Gets the value of the assets path.
        /// </summary>
        private static readonly string AssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assets");
    }
}