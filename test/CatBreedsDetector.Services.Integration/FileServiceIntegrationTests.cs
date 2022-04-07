namespace CatBreedsDetector.Services.Integration
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Xunit;
    using CatBreedsDetector.Tests.Common.Mocks;
    using CatBreedsDetector.Tests.Common.Helpers;

    public class FileServiceIntegrationTests : BaseTest
    {
        private const string TempDirectoryPath = "C:\\Temp\\TestFiles";
        
        [Theory]
        [MemberData(nameof(GetRandomFileNames))]
        public void DeleteFilesInDirectoryShouldDeleteNothingWithNonExistingDirectory(string randomFileName)
        {
            // Arrange
            var fileName = $"{TempDirectoryPath}\\{TestsHelper.GenerateRandomString()}";
            TestsHelper.CreateFileInDirectory(TempDirectoryPath, fileName);

            // Act
            this._fileService.DeleteFilesInDirectory(randomFileName);

            // Assert
            var existingFiles = Directory.GetFiles(TempDirectoryPath);
            Assert.NotEmpty(existingFiles);

            // Clean up
            File.Delete(fileName);
            Directory.Delete(TempDirectoryPath);
            Assert.False(Directory.Exists(TempDirectoryPath));
        }

        [Fact]
        public void DeleteFilesInDirectoryShouldDeleteTheFilesSuccessfully()
        {
            // Arrange
            var randomFilesCount = TestsHelper.GenerateRandomInteger();

            for (var i = 0; i <= randomFilesCount; i++)
            {
                var fileName = $"{TempDirectoryPath}\\{TestsHelper.GenerateRandomString()}";
                TestsHelper.CreateFileInDirectory(TempDirectoryPath, fileName);
            }

            // Act
            this._fileService.DeleteFilesInDirectory(TempDirectoryPath);

            // Assert
            var existingFiles = Directory.GetFiles(TempDirectoryPath);
            Assert.Empty(existingFiles);

            // Clean up
            Directory.Delete(TempDirectoryPath);
            Assert.False(Directory.Exists(TempDirectoryPath));
        }

        [Theory]
        [MemberData(nameof(GetInvalidImagesToSave))]
        public async Task SaveImageToFileAsyncShouldCreateNothingWithInvalidParameters(string imagePath, IFormFile imageFile)
        {
            // Arrange & Act
            await this._fileService.SaveImageToFileAsync(imagePath, imageFile);

            // Assert
            Assert.False(Directory.Exists(imagePath));
        }
        
        [Fact]
        public async Task SaveImageToFileAsyncShouldCreateImageFileCorrectly()
        {
            // Arrange
            var imageFile = FormFileMock.New.Object;
            Directory.CreateDirectory(TempDirectoryPath);
            
            var imagePath = $"{TempDirectoryPath}\\{TestsHelper.GenerateRandomString()}";
            
            // Act
            await this._fileService.SaveImageToFileAsync(imagePath, imageFile);

            // Assert
            Assert.True(Directory.Exists(TempDirectoryPath));
            Assert.NotEmpty(Directory.GetFiles(TempDirectoryPath));
            
            // Clean up
            File.Delete(imagePath);
            Directory.Delete(TempDirectoryPath);
            Assert.False(Directory.Exists(TempDirectoryPath));
        }

        public static IEnumerable<object[]> GetRandomFileNames()
        {
            yield return new[] { (string)null };
            yield return new[] { string.Empty };
            yield return new[] { TestsHelper.GenerateRandomString() };
        }
        
        public static IEnumerable<object[]> GetInvalidImagesToSave()
        {
            yield return new object[] { null, null };
            yield return new object[] { string.Empty, null };
            yield return new object[] { TestsHelper.GenerateRandomString(), null };
            yield return new object[] { null, FormFileMock.New.Object };
        }
    }
}
