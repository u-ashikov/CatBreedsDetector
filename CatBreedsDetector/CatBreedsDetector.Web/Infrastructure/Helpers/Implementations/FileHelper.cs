namespace CatBreedsDetector.Web.Infrastructure.Helpers.Implementations
{
    using Contracts;
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Threading.Tasks;

    public class FileHelper : IFileHelper
    {
        public void DeleteFilesInDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                var predictedDirectory = new DirectoryInfo(directoryPath);

                foreach (var file in predictedDirectory.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        public async Task SaveImageToFileAsync(string imagePath, IFormFile imageFile)
        {
            if (string.IsNullOrEmpty(imagePath) || imageFile == null)
            {
                return;
            }

            using (var stream = File.Create(imagePath))
            {
                await imageFile.CopyToAsync(stream);
            }
        }
    }
}
