namespace CatBreedsDetector.Web.Infrastructure.Helpers.Implementations
{
    using System.IO;
    using System.Threading.Tasks;
    using CatBreedsDetector.Web.Infrastructure.Helpers.Contracts;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// A custom implementation of the <see cref="IFileHelper"/> interface.
    /// </summary>
    public class FileHelper : IFileHelper
    {
        /// <inheritdoc />
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

        /// <inheritdoc />
        public async Task SaveImageToFileAsync(string imagePath, IFormFile imageFile)
        {
            if (string.IsNullOrEmpty(imagePath) || imageFile == null)
            {
                return;
            }

            using var stream = File.Create(imagePath);
            await imageFile.CopyToAsync(stream);
        }
    }
}
