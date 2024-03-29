namespace CatBreedsDetector.UnitTests;

using CatBreedsDetector.Common.Execution;
using CatBreedsDetector.Tests.Common.Helpers;
using Xunit;

public class ExecutionResultTests
{
    [Theory]
    [MemberData(nameof(GetInvalidErrorMessagesCollection))]
    public void Fail_ShouldThrowExceptionWithInvalidErrors(IEnumerable<string> errors)
    {
        // Arrange & Act & Assert
        Assert.Throws<InvalidOperationException>(() => ExecutionResult.Fail(errors.ToArray()));
    }
    
    [Fact]
    public void Fail_ShouldCreateNewInstanceWithErrorsAndWithUnsuccessfulState()
    {
        // Arrange & Act
        var randomErrorMessages = TestsHelper.GetRandomCountOf(() => TestsHelper.GenerateRandomString()).ToArray();
        var executionResult = ExecutionResult.Fail(randomErrorMessages);
        
        // Assert
        Assert.NotNull(executionResult);
        Assert.False(executionResult.IsSuccessful);
        Assert.NotEmpty(executionResult.Errors);
        Assert.Equal(randomErrorMessages, executionResult.Errors);
    }
    
    [Fact]
    public void Success_ShouldCreateNewInstanceWithoutErrorsAndWithSuccessState()
    {
        // Arrange & Act
        var executionResult = ExecutionResult.Success();
        
        // Assert
        Assert.NotNull(executionResult);
        Assert.True(executionResult.IsSuccessful);
        Assert.Empty(executionResult.Errors);
    }
    
    [Theory]
    [MemberData(nameof(GetInvalidErrorMessagesCollection))]
    public void FailWithOutcome_ShouldThrowExceptionWithInvalidErrors(IEnumerable<string> errors)
    {
        // Arrange & Act & Assert
        Assert.Throws<InvalidOperationException>(() => ExecutionResult<string>.Fail(errors.ToArray()));
    }
    
    [Fact]
    public void FailWithOutcome_ShouldCreateNewInstanceWithErrorsAndWithUnsuccessfulState()
    {
        // Arrange & Act
        var randomErrorMessages = TestsHelper.GetRandomCountOf(() => TestsHelper.GenerateRandomString()).ToArray();
        var executionResult = ExecutionResult<string>.Fail(randomErrorMessages);
        
        // Assert
        Assert.NotNull(executionResult);
        Assert.False(executionResult.IsSuccessful);
        Assert.NotEmpty(executionResult.Errors);
        Assert.Equal(randomErrorMessages, executionResult.Errors);
        Assert.Null(executionResult.Outcome);
    }

    [Fact]
    public void SuccessWith_ShouldThrowArgumentNullExceptionWithInvalidOutcome()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => ExecutionResult<DummyResult>.SuccessWith(null!));
    }
    
    [Fact]
    public void SuccessWith_ShouldCreateNewInstanceWithoutErrorsAndWithSuccessState()
    {
        // Arrange & Act
        var dummyResult = new DummyResult(TestsHelper.GenerateRandomString(), TestsHelper.GenerateRandomString());
        var executionResult = ExecutionResult<DummyResult>.SuccessWith(dummyResult);

        // Assert
        Assert.NotNull(executionResult);
        Assert.True(executionResult.IsSuccessful);
        Assert.Empty(executionResult.Errors);
        Assert.Equal(dummyResult, executionResult.Outcome);
    }

    public static IEnumerable<object[]> GetInvalidErrorMessagesCollection()
    {
        yield return new object[] { new[] { (string)null! } };
        yield return new object[] { new[] { string.Empty } };
        yield return new object[] { new[] { TestsHelper.GenerateRandomString(), string.Empty } };
        yield return new object[] { new[] { TestsHelper.GenerateRandomString(), TestsHelper.GenerateWhiteSpaces(10) } };
        yield return new object[] { Array.Empty<string>() };
    }

    private class DummyResult
    {
        public DummyResult(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        
        public string Id { get; }
        
        public string Name { get; }
    }
}