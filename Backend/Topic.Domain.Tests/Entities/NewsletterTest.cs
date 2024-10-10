using Topic.Domain.Entities;
using Topic.Domain.Enums;

namespace Topic.Domain.Tests.Entities;

public class NewsletterTest
{
    [Fact]
    public void Should_Return_Success_When_All_Fields_Correct()
    {
        // Arrange
        string title = "Title";
        StatusEnum status = StatusEnum.Pending;
        string[] keywords = ["a", "b"];

        // Act
        var newsletter = Newsletter.Create(title, status, keywords);

        // Assert
        Assert.True(newsletter.IsValid);
        Assert.False(newsletter.Errors.Any());
    }

    [Fact]
    public void Should_Return_Error_When_Title_Is_Empty()
    {
        // Arrange
        string title = "";
        StatusEnum status = StatusEnum.Pending;
        string[] keywords = ["a", "b"];

        // Act
        var newsletter = Newsletter.Create(title, status, keywords);

        Assert.False(newsletter.IsValid);
        Assert.Collection(newsletter.Errors, error1 =>
        {
            Assert.Equal("NotEmptyValidator", error1.ErrorCode);
        });
    }

    [Fact]
    public void Should_Return_Error_When_Keywords_Is_Empty()
    {
        // Arrange
        string title = "Title";
        StatusEnum status = StatusEnum.Pending;
        string[] keywords = [];

        // Act
        var newsletter = Newsletter.Create(title, status, keywords);

        Assert.False(newsletter.IsValid);
        Assert.Collection(newsletter.Errors, error1 =>
        {
            Assert.Equal("NotEmptyValidator", error1.ErrorCode);
        });
    }
}
