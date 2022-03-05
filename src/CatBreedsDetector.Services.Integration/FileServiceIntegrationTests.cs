namespace CatBreedsDetector.Services.Integration
{
    using System.Collections.Generic;
    using System.IO;
    using Xunit;
    using CatBreedsDetector.Tests.Common.Helpers;

    public class FileServiceIntegrationTests : BaseTest
    {
        [Theory]
        [MemberData(nameof(GetRandomFileNames))]
        public void DeleteFilesInDirectoryShouldDeleteNothingWithNonExistingDirectory(string randomFileName)
        {
            // Arrange
            var filePath = "c:\\Temp\\TestFiles";
            var fileName = $"{filePath}\\{TestsHelpers.GenerateRandomString()}";
            this.CreateFileInDirectory(filePath, fileName);

            // Act
            this._fileService.DeleteFilesInDirectory(randomFileName);

            // Assert
            var existingFiles = Directory.GetFiles(filePath);
            Assert.NotEmpty(existingFiles);

            // Clean up
            File.Delete(fileName);
            Directory.Delete(filePath);
            Assert.False(Directory.Exists(filePath));
        }

        [Fact]
        public void DeleteFilesInDirectoryShouldDeleteTheFilesSuccessfully()
        {
            // Arrange
            var filePath = "c:\\Temp\\TestFiles";
            var fileName = $"{filePath}\\{TestsHelpers.GenerateRandomString()}";
            this.CreateFileInDirectory(filePath, fileName);

            // Act
            this._fileService.DeleteFilesInDirectory(filePath);

            // Assert
            var existingFiles = Directory.GetFiles(filePath);
            Assert.Empty(existingFiles);

            // Clean up
            Directory.Delete(filePath);
            Assert.False(Directory.Exists(filePath));
        }

        public static IEnumerable<object[]> GetRandomFileNames()
        {
            yield return new[] { (string)null };
            yield return new[] { string.Empty };
            yield return new[] { TestsHelpers.GenerateRandomString() };
        }

        private void CreateFileInDirectory(string filePath, string fileName)
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
                if (fileStream != null)
                    fileStream.Dispose();
            }
        }
    }
}
