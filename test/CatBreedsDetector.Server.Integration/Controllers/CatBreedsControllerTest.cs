namespace CatBreedsDetector.Server.Integration.Controllers
{
    using System.IO;
    using System.Threading;
    using CatBreedsDetector.Tests.Common.Helpers;
    using CatBreedsDetector.Tests.Common.Mocks;
    using System.Collections.Generic;
    using CatBreedsDetector.Web.Controllers;
    using CatBreedsDetector.Models.InputModels;
    using CatBreedsDetector.Models.ViewModels;
    using MyTested.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc.Builders.Contracts.Controllers;
    using Xunit;
    
    public class CatBreedsControllerTest
    {
        private const string DirectoryPath = "C:\\Temp\\TestFiles";
        
        [Theory]
        [MemberData(nameof(PrepareInvalidCatBreedDetectionInputModels))]
        public void DetectAsyncWithNullModelShouldReturnBadRequest(CatBreedDetectInputModel inputModel)
        {
            // Arrange & Act & Assert
            PrepareTestController()
                .Calling(c => c.DetectAsync(inputModel, CancellationToken.None))
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void DetectAsyncShouldWorkCorrectly()
        {
            // Arrange
            var createTestFilesDirectory = Directory.CreateDirectory(DirectoryPath);
            var createPredictedImagesDirectory = Directory.CreateDirectory(Path.Combine(DirectoryPath, "PredictedImages"));
            
            var mockedFormFile = FormFileMock.New;
            var randomFileName = $"{TestsHelper.GenerateRandomString()}.jpg";

            mockedFormFile.SetupGet(f => f.FileName).Returns(randomFileName);
            
            var inputModel = new CatBreedDetectInputModel() { CatImage = mockedFormFile.Object };
            
            // Act & Assert.
            PrepareTestController()
                .Calling(c => c.DetectAsync(inputModel, CancellationToken.None))
                .ShouldReturn()
                .Ok(result => result.WithModelOfType<CatBreedPredictionResultModel>());
            
            // Cleanup
            File.Delete(Path.Combine(createPredictedImagesDirectory.FullName, inputModel.CatImage.FileName));
            Directory.Delete(createPredictedImagesDirectory.FullName);
            Directory.Delete(createTestFilesDirectory.FullName);
        }

        public static IEnumerable<object[]> PrepareInvalidCatBreedDetectionInputModels()
        {
            yield return new object[] { null };
            yield return new object[] { new CatBreedDetectInputModel() };
            yield return new object[] { new CatBreedDetectInputModel() {CatImage = FormFileMock.New.Object }};
        }

        private static IControllerBuilder<CatBreedsController> PrepareTestController()
            => MyMvc.Controller<CatBreedsController>();
    }
}