using ReporterApp.Core.Reports;
using System.Text;

namespace ReporterApp.UnitTests.Core.Reports;

public class StringBuilderExtensionTests
{
    #region RemoveAllCr Tests

    [Fact]
    public void RemoveAllCr_StringWithCarriageReturns_RemovesAllCr()
    {
        // Arrange
        var sb = new StringBuilder("Hello\rWorld\r\nTest\r");

        // Act
        var result = sb.RemoveAllCr();

        // Assert
        Assert.Equal("HelloWorld\nTest", result.ToString());
    }

    [Fact]
    public void RemoveAllCr_StringWithoutCarriageReturns_Unchanged()
    {
        // Arrange
        var sb = new StringBuilder("Hello World\nTest");

        // Act
        var result = sb.RemoveAllCr();

        // Assert
        Assert.Equal("Hello World\nTest", result.ToString());
    }

    [Fact]
    public void RemoveAllCr_EmptyStringBuilder_EmptyResult()
    {
        // Arrange
        var sb = new StringBuilder();

        // Act
        var result = sb.RemoveAllCr();

        // Assert
        Assert.Equal(string.Empty, result.ToString());
    }

    [Fact]
    public void RemoveAllCr_OnlyCarriageReturns_EmptyResult()
    {
        // Arrange
        var sb = new StringBuilder("\r\r\r");

        // Act
        var result = sb.RemoveAllCr();

        // Assert
        Assert.Equal(string.Empty, result.ToString());
    }

    [Fact]
    public void RemoveAllCr_ReturnsSameStringBuilder_InstanceNotChanged()
    {
        // Arrange
        var sb = new StringBuilder("Test");

        // Act
        var result = sb.RemoveAllCr();

        // Assert
        Assert.Same(sb, result);
    }

    #endregion

    #region EnsureLastLfRemoved Tests

    [Fact]
    public void EnsureLastLfRemoved_StringWithTrailingLf_RemovesLastLf()
    {
        // Arrange
        var sb = new StringBuilder("Hello World\n");

        // Act
        var result = sb.EnsureLastLfRemoved();

        // Assert
        Assert.Equal("Hello World", result.ToString());
    }

    [Fact]
    public void EnsureLastLfRemoved_StringWithoutTrailingLf_Unchanged()
    {
        // Arrange
        var sb = new StringBuilder("Hello World");

        // Act
        var result = sb.EnsureLastLfRemoved();

        // Assert
        Assert.Equal("Hello World", result.ToString());
    }

    [Fact]
    public void EnsureLastLfRemoved_EmptyStringBuilder_EmptyResult()
    {
        // Arrange
        var sb = new StringBuilder();

        // Act
        var result = sb.EnsureLastLfRemoved();

        // Assert
        Assert.Equal(string.Empty, result.ToString());
    }

    [Fact]
    public void EnsureLastLfRemoved_MultipleLines_RemovesOnlyLastLf()
    {
        // Arrange
        var sb = new StringBuilder("Line1\nLine2\nLine3\n");

        // Act
        var result = sb.EnsureLastLfRemoved();

        // Assert
        Assert.Equal("Line1\nLine2\nLine3", result.ToString());
    }

    [Fact]
    public void EnsureLastLfRemoved_StringWithCrAndLf_OnlyLfRemoved()
    {
        // Arrange
        var sb = new StringBuilder("Hello\r\n");

        // Act
        var result = sb.EnsureLastLfRemoved();

        // Assert
        Assert.Equal("Hello\r", result.ToString());
    }

    [Fact]
    public void EnsureLastLfRemoved_ReturnsSameStringBuilder_InstanceNotChanged()
    {
        // Arrange
        var sb = new StringBuilder("Test");

        // Act
        var result = sb.EnsureLastLfRemoved();

        // Assert
        Assert.Same(sb, result);
    }

    [Fact]
    public void EnsureLastLfRemoved_OnlyLf_EmptyResult()
    {
        // Arrange
        var sb = new StringBuilder("\n");

        // Act
        var result = sb.EnsureLastLfRemoved();

        // Assert
        Assert.Equal(string.Empty, result.ToString());
    }

    #endregion

    #region Combined Tests

    [Fact]
    public void RemoveAllCr_And_EnsureLastLfRemoved_CleansStringCompletely()
    {
        // Arrange
        var sb = new StringBuilder("Line1\r\nLine2\r\nLine3\r\n");

        // Act
        var result = sb.RemoveAllCr().EnsureLastLfRemoved();

        // Assert
        Assert.Equal("Line1\nLine2\nLine3", result.ToString());
    }

    [Fact]
    public void EnsureLastLfRemoved_And_RemoveAllCr_DifferentOrder_SameResult()
    {
        // Arrange
        var text = "Line1\r\nLine2\r\n";

        // Act
        var result1 = new StringBuilder(text).EnsureLastLfRemoved().RemoveAllCr();
        var result2 = new StringBuilder(text).RemoveAllCr().EnsureLastLfRemoved();

        // Assert
        Assert.Equal(result1.ToString(), result2.ToString());
    }

    #endregion
}
