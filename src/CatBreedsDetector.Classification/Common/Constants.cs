namespace CatBreedsDetector.Classification.Common;

using System.IO;
using System.Reflection;

/// <summary>
/// A class containing common constants for the application.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Gets the name of the saved trained model as a zip archive.
    /// </summary>
    public const string SavedModelFileName = "model.zip";

    /// <summary>
    /// Gets the images folder path.
    /// </summary>
    public static readonly string ImagesFolder;

    /// <summary>
    /// Gets the training tags path.
    /// </summary>
    public static readonly string TrainTagsTsv;

    /// <summary>
    /// Gets the tensor flow model path.
    /// </summary>
    public static readonly string InceptionTensorFlowModel;

    static Constants()
    {
        var assetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assets");

        ImagesFolder = Path.Combine(assetsPath, "images");
        TrainTagsTsv = Path.Combine(ImagesFolder, "tags.tsv");
        InceptionTensorFlowModel = Path.Combine(assetsPath, "inception", "tensorflow_inception_graph.pb");
    }
}