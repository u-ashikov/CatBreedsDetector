namespace CatBreedsDetector.Services.Implementations;

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Http;
using CatBreedsDetector.Common.Execution;
using Constants = CatBreedsDetector.Common.Constants;

/// <summary>
/// A custom implementation of the <see cref="IFileService"/> interface.
/// </summary>
public class FileService : IFileService
{
    /// <inheritdoc />
    public ExecutionResult DeleteFilesInDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            return ExecutionResult.Fail(Constants.Message.DirectoryDoesNotExist);
            
        var predictedDirectory = new DirectoryInfo(directoryPath);

        foreach (var file in predictedDirectory.GetFiles())
            file.Delete();

        return ExecutionResult.Success();
    }

    /// <inheritdoc />
    public async Task<ExecutionResult> SaveImageToFileAsync(string imagePath, IFormFile imageFile, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(imagePath))
            return ExecutionResult.Fail(Constants.Message.InvalidImagePath);
            
        if (imageFile is null)
            return ExecutionResult.Fail(Constants.Message.InvalidImageFile);

        await using var stream = File.Create(imagePath);
        await imageFile.CopyToAsync(stream, cancellationToken);

        return ExecutionResult.Success();
    }
}