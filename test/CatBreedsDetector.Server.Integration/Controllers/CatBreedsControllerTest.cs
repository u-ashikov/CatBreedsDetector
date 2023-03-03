namespace CatBreedsDetector.Server.Integration.Controllers
{
    using System.IO;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using CatBreedsDetector.Tests.Common;
    using CatBreedsDetector.Tests.Common.Helpers;
    using CatBreedsDetector.Tests.Common.Mocks;
    using System.Collections.Generic;
    using CatBreedsDetector.Models.InputModels;
    using CatBreedsDetector.Models.ViewModels;
    using Xunit;

    public class CatBreedsControllerTest : BaseControllerTests
    {
        public CatBreedsControllerTest(CustomWebApplicationFactory<Program> webApplicationFactory)
            : base(webApplicationFactory)
        {
        }

        [Theory]
        [MemberData(nameof(PrepareInvalidCatBreedDetectionInputModels))]
        public async Task DetectAsync_ShouldReturnBadRequestWithNullModel(CatBreedDetectInputModel inputModel)
        {
            // Arrange & Act
            var url = UrlBuilder.Build(EndpointsConstants.ApiUrl, EndpointsConstants.DetectCatBreedUrl);
            var response = await this.HttpClient.PostAsJsonAsync(url, inputModel);

            // Assert
            response.AssertUnsuccessfulStatusCode();
        }

        [Theory]
        [InlineData("jpg")]
        [InlineData("jpeg")]
        [InlineData("png")]
        [InlineData("gif")]
        public async Task DetectAsync_ShouldWorkCorrectly(string fileExtension)
        {
            // Arrange
            var testFilesDirectory = TestsHelper.CreateTestDirectory(TestsConstants.TestFilesDirectoryName);
            var createPredictedImagesDirectory = Directory.CreateDirectory(Path.Combine(testFilesDirectory, "PredictedImages"));
            
            var randomFileName = $"{TestsHelper.GenerateRandomString()}.{fileExtension}";
            var randomFileContent = TestsHelper.GenerateRandomString();

            var formData = TestsHelper.CreateFormDataFileContent(randomFileName, nameof(CatBreedDetectInputModel.CatImage), randomFileContent);

            var url = UrlBuilder.Build(EndpointsConstants.ApiUrl, EndpointsConstants.DetectCatBreedUrl);
            var response = await this.HttpClient.PostAsync(url, formData);

            await response.AssertSuccessStatusCodeAndResult<CatBreedPredictionResultModel>();

            // Cleanup
            File.Delete(Path.Combine(createPredictedImagesDirectory.FullName, randomFileName));
            Directory.Delete(createPredictedImagesDirectory.FullName);
            Directory.Delete(testFilesDirectory);
        }

        public static IEnumerable<object[]> PrepareInvalidCatBreedDetectionInputModels()
        {
            yield return new object[] {null};
            yield return new object[] {new CatBreedDetectInputModel()};
            yield return new object[] {new CatBreedDetectInputModel() {CatImage = FormFileMock.New.Object}};
        }
    }
}