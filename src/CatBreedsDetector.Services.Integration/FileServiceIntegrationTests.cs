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
        private const string DirectoryPath = "C:\\Temp\\TestFiles";
        
        [Theory]
        [MemberData(nameof(GetRandomFileNames))]
        public void DeleteFilesInDirectoryShouldDeleteNothingWithNonExistingDirectory(string randomFileName)
        {
            // Arrange
            var fileName = $"{DirectoryPath}\\{TestsHelper.GenerateRandomString()}";
            CreateFileInDirectory(DirectoryPath, fileName);

            // Act
            this._fileService.DeleteFilesInDirectory(randomFileName);

            // Assert
            var existingFiles = Directory.GetFiles(DirectoryPath);
            Assert.NotEmpty(existingFiles);

            // Clean up
            File.Delete(fileName);
            Directory.Delete(DirectoryPath);
            Assert.False(Directory.Exists(DirectoryPath));
        }

        [Fact]
        public void DeleteFilesInDirectoryShouldDeleteTheFilesSuccessfully()
        {
            // Arrange
            var randomFilesCount = TestsHelper.GenerateRandomInteger();

            for (var i = 0; i <= randomFilesCount; i++)
            {
                var fileName = $"{DirectoryPath}\\{TestsHelper.GenerateRandomString()}";
                CreateFileInDirectory(DirectoryPath, fileName);
            }

            // Act
            this._fileService.DeleteFilesInDirectory(DirectoryPath);

            // Assert
            var existingFiles = Directory.GetFiles(DirectoryPath);
            Assert.Empty(existingFiles);

            // Clean up
            Directory.Delete(DirectoryPath);
            Assert.False(Directory.Exists(DirectoryPath));
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
            Directory.CreateDirectory(DirectoryPath);
            var imagePath = $"{DirectoryPath}\\{TestsHelper.GenerateRandomString()}";
            
            // Act
            await this._fileService.SaveImageToFileAsync(imagePath, imageFile);

            // Assert
            Assert.True(Directory.Exists(DirectoryPath));
            Assert.NotEmpty(Directory.GetFiles(DirectoryPath));
            
            // Clean up
            File.Delete(imagePath);
            Directory.Delete(DirectoryPath);
            Assert.False(Directory.Exists(DirectoryPath));
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

        private static void CreateFileInDirectory(string filePath, string fileName)
        {
            if (Directory.Exists(filePath) == false)
                Directory.CreateDirectory(filePath);

            FileStream fileStream = null;

            try
            {
                fileStream = File.Create(Path.Combine(fileName));
            }
            finally
            {
                fileStream?.Dispose();
            }
        }
    }
}
