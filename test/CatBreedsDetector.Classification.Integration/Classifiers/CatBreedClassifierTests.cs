namespace CatBreedsDetector.Classification.Integration.Classifiers;

using Xunit;
using CatBreedsDetector.Tests.Common.Helpers;

public class CatBreedClassifierTests
{
    private const string TempDirectoryPath = "C:\\Temp\\TestFiles";
    
    [Theory]
    [MemberData(nameof(GetInvalidImageClassificationParameters))]
    public void ClassifySingleImageShouldThrowExceptionWithInvalidArguments(string path)
    {
        // Arrange
        var catBreedClassifier = new CatBreedClassifier();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => catBreedClassifier.ClassifySingleImage(path));
    }

    public static IEnumerable<object[]> GetInvalidImageClassificationParameters()
    {
        yield return new[] { (string)null };
        yield return new[] { string.Empty };
        yield return new[] {TestsHelper.GenerateWhiteSpaces()};
        yield return new[] {TestsHelper.GenerateRandomString()};
    }
}