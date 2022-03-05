namespace CatBreedsDetector.Services.Integration
{
    using CatBreedsDetector.Services.Contracts;
    using CatBreedsDetector.Services.Implementations;

    public abstract class BaseTest
    {
        protected readonly IFileService _fileService;

        public BaseTest()
        {
            this._fileService = new FileService();
        }
    }
}
