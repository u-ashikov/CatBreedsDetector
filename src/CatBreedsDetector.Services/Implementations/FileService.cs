namespace CatBreedsDetector.Services.Implementations
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using CatBreedsDetector.Services.Contracts;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// A custom implementation of the <see cref="IFileService"/> interface.
    /// </summary>
    public class FileService : IFileService
    {
        /// <inheritdoc />
        public void DeleteFilesInDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                var predictedDirectory = new DirectoryInfo(directoryPath);

                foreach (var file in predictedDirectory.GetFiles())
                    file.Delete();
            }
        }

        /// <inheritdoc />
        public async Task SaveImageToFileAsync(string imagePath, IFormFile imageFile, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(imagePath) || imageFile == null)
                return;

            using var stream = File.Create(imagePath);
            await imageFile.CopyToAsync(stream, cancellationToken);
        }
    }
}
