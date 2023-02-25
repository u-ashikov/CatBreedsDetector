namespace CatBreedsDetector.Tests.Common.Mocks
{
    using Microsoft.AspNetCore.Http;
    using Moq;
    
    public class FormFileMock
    {
        public static Mock<IFormFile> New => new();
    }
}