namespace CatBreedsDetector.Server.Integration.Controllers
{
    using System.IO;
    using CatBreedsDetector.Models;
    using CatBreedsDetector.Tests.Common.Helpers;
    using CatBreedsDetector.Tests.Common.Mocks;
    using System.Collections.Generic;
    using CatBreedsDetector.Models.Models;
    using CatBreedsDetector.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers;
    using System.Threading.Tasks;
    using Xunit;
    
    public class CatBreedsControllerTest
    {
        private const string DirectoryPath = "C:\\Temp\\TestFiles";
        
        [Theory]
        [MemberData(nameof(PrepareInvalidCatBreedDetectionInputModels))]
        public async Task DetectAsyncWithNullModelShouldReturnBadRequest(CatBreedDetectInputModel inputModel)
        {
            // Arrange & Act & Assert
            this.PrepareTestController()
                .Calling(c => c.DetectAsync(inputModel))
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public async Task DetectAsyncShouldWorkCorrectly()
        {
            // Arrange
            var createTestFilesDirectory = Directory.CreateDirectory(DirectoryPath);
            var createPredictedImagesDirectory = Directory.CreateDirectory(Path.Combine(DirectoryPath, "PredictedImages"));
            
            var mockedFormFile = FormFileMock.New;
            mockedFormFile.SetupGet(f => f.FileName).Returns(TestsHelper.GenerateRandomString());
            
            var inputModel = new CatBreedDetectInputModel() { CatImage = mockedFormFile.Object };
            
            // Act & Assert.
            this.PrepareTestController()
                .Calling(c => c.DetectAsync(inputModel))
                .ShouldReturn()
                .Ok(result => result.WithModelOfType<CatBreedPredictionResultModel>());
            
            // Cleanup
            File.Delete(Path.Combine(createPredictedImagesDirectory.FullName, inputModel.CatImage.FileName));
            Directory.Delete(createPredictedImagesDirectory.FullName);
            Directory.Delete(createTestFilesDirectory.FullName);
        }

        public static IEnumerable<object[]> PrepareInvalidCatBreedDetectionInputModels()
        {
            yield return new[] {(CatBreedDetectInputModel)null};
            yield return new[] { new CatBreedDetectInputModel() };
            yield return new[] {new CatBreedDetectInputModel() {CatImage = FormFileMock.New.Object }};
        }

        private IControllerBuilder<CatBreedsController> PrepareTestController()
            => MyMvc.Controller<CatBreedsController>();
    }
}