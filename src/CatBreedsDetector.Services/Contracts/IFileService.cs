namespace CatBreedsDetector.Services.Contracts
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using CatBreedsDetector.Common.Execution;

    /// <summary>
    /// An interface defining the structure of a component responsible for executing operations with files.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Use this method to delete all files in a given directory.
        /// </summary>
        /// <param name="directoryPath">The path to the directory which files should be deleted.</param>
        /// <returns>An <see cref="ExecutionResult"/> containing the state of the operation.</returns>
        ExecutionResult DeleteFilesInDirectory(string directoryPath);

        /// <summary>
        /// Use this method to save an image to a file.
        /// </summary>
        /// <param name="imagePath">The path to the image.</param>
        /// <param name="imageFile">The image file that should be stored.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>A <see cref="Task"/> containing the <see cref="ExecutionResult"/> and representing the asynchronous state of the operation.</returns>
        Task<ExecutionResult> SaveImageToFileAsync(string imagePath, IFormFile imageFile, CancellationToken cancellationToken);
    }
}
