namespace CatBreedsDetector.Tests.Common.Mocks;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CatBreedsDetector.Services.Contracts;

public class FileServiceMock : IFileService
{
    /// <inheritdoc />
    public void DeleteFilesInDirectory(string directoryPath)
    {
    }

    /// <inheritdoc />
    public Task SaveImageToFileAsync(string imagePath, IFormFile imageFile, CancellationToken cancellationToken) => Task.CompletedTask;
}