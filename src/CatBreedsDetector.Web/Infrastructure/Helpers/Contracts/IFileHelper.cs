namespace CatBreedsDetector.Web.Infrastructure.Helpers.Contracts
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// An interface defining the structure of a component responsible for executing operations with files.
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// Use this method to delete all files in a given directory.
        /// </summary>
        /// <param name="directoryPath">The path to the directory which files should be deleted.</param>
        void DeleteFilesInDirectory(string directoryPath);

        /// <summary>
        /// Use this method to save an image to a file.
        /// </summary>
        /// <param name="imagePath">The path to the image.</param>
        /// <param name="imageFile">The image file that should be stored.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous state of the operation.</returns>
        Task SaveImageToFileAsync(string imagePath, IFormFile imageFile);
    }
}
