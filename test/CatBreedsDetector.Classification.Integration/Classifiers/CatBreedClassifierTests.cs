namespace CatBreedsDetector.Classification.Integration.Classifiers;

using System;
using System.Collections.Generic;
using Xunit;
using CatBreedsDetector.Tests.Common.Helpers;

public class CatBreedClassifierTests
{
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
        yield return new object[] { null };
        yield return new object[] { string.Empty };
        yield return new object[] {TestsHelper.GenerateWhiteSpaces()};
        yield return new object[] {TestsHelper.GenerateRandomString()};
    }
}