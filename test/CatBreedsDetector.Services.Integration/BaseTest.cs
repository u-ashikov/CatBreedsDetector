namespace CatBreedsDetector.Services.Integration;

using CatBreedsDetector.Services.Contracts;
using CatBreedsDetector.Services.Implementations;

public abstract class BaseTest
{
    protected readonly IFileService FileService;

    protected BaseTest()
    {
        this.FileService = new FileService();
    }
}