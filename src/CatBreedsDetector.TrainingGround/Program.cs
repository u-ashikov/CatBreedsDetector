namespace CatBreedsDetector.TrainingGround;

using System.IO;

public class Program
{
    public static void Main()
    {
        const string catsImagesDirectoryName = "cats";
        const string trainingTagsFile = "tags.tsv";
        const string trainingTagsSeparatorChar = "\t";

        if (!Directory.Exists(catsImagesDirectoryName))
            return;

        var directories = Directory.GetDirectories(catsImagesDirectoryName);

        using var fileWriter = new StreamWriter(trainingTagsFile, true);
        foreach (var directory in directories)
        {
            var directoryInfo = new DirectoryInfo(directory);
            var files = Directory.GetFiles(directory);

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                fileWriter.WriteLine(string.Concat(fileInfo.Name, trainingTagsSeparatorChar, directoryInfo.Name));
            }
        }
    }
}