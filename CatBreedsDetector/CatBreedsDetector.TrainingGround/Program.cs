using System;
using System.IO;

namespace CatBreedsDetector.TrainingGround
{
    public class Program
    {
        public static void Main()
        {
            if (!Directory.Exists("cats"))
            {
                return;
            }

            var directories = Directory.GetDirectories("cats");

            using (var fileWriter = new StreamWriter("tags.tsv", true))
            {
                foreach (var directory in directories)
                {
                    var directoryInfo = new DirectoryInfo(directory);
                    var files = Directory.GetFiles(directory);

                    foreach (var file in files)
                    {
                        var fileInfo = new FileInfo(file);
                        
                        fileWriter.WriteLine(string.Concat(fileInfo.Name, "\t", directoryInfo.Name));
                    }
                }
            }
        }
    }
}
