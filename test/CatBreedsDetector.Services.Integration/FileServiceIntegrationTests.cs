namespace CatBreedsDetector.Services.Integration;

using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;
using CatBreedsDetector.Tests.Common;
using CatBreedsDetector.Tests.Common.Mocks;
using CatBreedsDetector.Tests.Common.Helpers;

public class FileServiceIntegrationTests : BaseTest
{
    [Theory]
    [MemberData(nameof(GetRandomDirectoryNames))]
    public void DeleteFilesInDirectory_ShouldDeleteNothingWithNonExistingDirectory(string randomDirectoryName)
    {
        // Arrange
        var testFilesDirectory = TestsHelper.CreateTestDirectory(TestsConstants.TestFilesDirectoryName);
        var fileName = $"{testFilesDirectory}\\{TestsHelper.GenerateRandomString()}";
        TestsHelper.CreateFileInDirectory(testFilesDirectory, fileName);

        // Act
        this.FileService.DeleteFilesInDirectory(randomDirectoryName);

        // Assert
        var existingFiles = Directory.GetFiles(testFilesDirectory);
        Assert.NotEmpty(existingFiles);

        // Clean up
        File.Delete(fileName);
        Directory.Delete(testFilesDirectory);
        Assert.False(Directory.Exists(testFilesDirectory));
    }

    [Fact]
    public void DeleteFilesInDirectory_ShouldDeleteTheFilesSuccessfully()
    {
        // Arrange
        var testFilesDirectory = TestsHelper.CreateTestDirectory(TestsConstants.TestFilesDirectoryName);
        var randomFilesCount = TestsHelper.GenerateRandomInteger();

        for (var i = 0; i <= randomFilesCount; i++)
        {
            var fileName = $"{testFilesDirectory}\\{TestsHelper.GenerateRandomString()}";
            TestsHelper.CreateFileInDirectory(testFilesDirectory, fileName);
        }

        // Act
        this.FileService.DeleteFilesInDirectory(testFilesDirectory);

        // Assert
        var existingFiles = Directory.GetFiles(testFilesDirectory);
        Assert.Empty(existingFiles);

        // Clean up
        Directory.Delete(testFilesDirectory);
        Assert.False(Directory.Exists(testFilesDirectory));
    }

    [Theory]
    [MemberData(nameof(GetInvalidImagesToSave))]
    public async Task SaveImageToFileAsyncShouldCreateNothingWithInvalidParameters(string imagePath, IFormFile imageFile)
    {
        // Arrange & Act
        await this.FileService.SaveImageToFileAsync(imagePath, imageFile, CancellationToken.None);

        // Assert
        Assert.False(Directory.Exists(imagePath));
    }
        
    [Fact]
    public async Task SaveImageToFileAsyncShouldCreateImageFileCorrectly()
    {
        // Arrange
        var imageFile = FormFileMock.New.Object;
        var testFilesDirectory = TestsHelper.CreateTestDirectory(TestsConstants.TestFilesDirectoryName);
            
        var imagePath = $"{testFilesDirectory}\\{TestsHelper.GenerateRandomString()}";
            
        // Act
        await this.FileService.SaveImageToFileAsync(imagePath, imageFile, CancellationToken.None);

        // Assert
        Assert.True(Directory.Exists(testFilesDirectory));
        Assert.NotEmpty(Directory.GetFiles(testFilesDirectory));
            
        // Clean up
        File.Delete(imagePath);
        Directory.Delete(testFilesDirectory);
        Assert.False(Directory.Exists(testFilesDirectory));
    }

    public static IEnumerable<object[]> GetRandomDirectoryNames()
    {
        yield return new object[] { null };
        yield return new object[] { string.Empty };
        yield return new object[] { TestsHelper.GenerateRandomString() };
    }
        
    public static IEnumerable<object[]> GetInvalidImagesToSave()
    {
        yield return new object[] { null, null };
        yield return new object[] { string.Empty, null };
        yield return new object[] { TestsHelper.GenerateRandomString(), null };
        yield return new object[] { null, FormFileMock.New.Object };
    }
}