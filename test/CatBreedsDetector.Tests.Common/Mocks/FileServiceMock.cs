namespace CatBreedsDetector.Tests.Common.Mocks;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CatBreedsDetector.Services.Contracts;
using CatBreedsDetector.Common.Execution;

public class FileServiceMock : IFileService
{
    /// <inheritdoc />
    public ExecutionResult DeleteFilesInDirectory(string directoryPath) => ExecutionResult.Success();

    /// <inheritdoc />
    public Task<ExecutionResult> SaveImageToFileAsync(string imagePath, IFormFile imageFile, CancellationToken cancellationToken)
        => Task.FromResult(ExecutionResult.Success());
}