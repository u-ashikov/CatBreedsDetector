namespace CatBreedsDetector.TrainingGround
{
    using System.IO;

    public class Program
    {
        public static void Main()
        {
            const string CatsImagesDirectoryName = "cats";
            const string TrainingTagsFile = "tags.tsv";
            const string TrainingTagsSeparatorChar = "\t";

            if (!Directory.Exists(CatsImagesDirectoryName))
            {
                return;
            }

            var directories = Directory.GetDirectories(CatsImagesDirectoryName);

            using (var fileWriter = new StreamWriter(TrainingTagsFile, true))
            {
                foreach (var directory in directories)
                {
                    var directoryInfo = new DirectoryInfo(directory);
                    var files = Directory.GetFiles(directory);

                    foreach (var file in files)
                    {
                        var fileInfo = new FileInfo(file);

                        fileWriter.WriteLine(string.Concat(fileInfo.Name, TrainingTagsSeparatorChar, directoryInfo.Name));
                    }
                }
            }
        }
    }
}
