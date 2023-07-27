using FluentAssertions;
using WebApi.BookOperations;
using static WebApi.BookOperations.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands;

public class CreateBookComandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("Lord Of The Rings", 0, 0 ,0)]
    [InlineData("Lord Of The Rings", 100, 0, 0)]
    [InlineData("Lord Of The Rings", 0, 1, 0)]
    [InlineData("Lord Of The Rings", 0, 0, 1)]
    [InlineData("Lord Of The Rings", 100, 1, 0)]
    [InlineData("Lord Of The Rings", 100, 0, 1)]
    [InlineData("", 0, 0, 0)]
    [InlineData("", 100, 0, 0)]
    [InlineData("", 0, 1, 0)]
    [InlineData("", 0, 0, 1)]
    [InlineData("", 100, 1, 1)]
    [InlineData("", 100, 1, 0)]
    [InlineData("", 100, 0, 1)]
    [InlineData("Lor", 100, 1, 1)]
    [InlineData("Lor", 0, 1, 1)]
    [InlineData("Lor", 100, 0, 0)]
    [InlineData("Lor", 0, 1, 0)]
    [InlineData("Lor", 0, 0, 1)]
    [InlineData("Lord", 0, 0, 0)]
    [InlineData("Lord", 100, 0, 0)]
    [InlineData("Lord", 100, 1, 0)]
    [InlineData("Lord", 100, 0, 1)]
    [InlineData(" ", 100, 1, 1)]
    [InlineData(" ", 0, 1, 1)]
    [InlineData(" ", 0, 0, 1)]
    [InlineData(" ", 0, 1, 0)]
    [InlineData(" ", 100, 0, 0)]
    public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
    {
        // arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = genreId,
            AuthorId = authorId
        };

        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
    {
        // arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = "Test Title",
            PageCount = 100,
            PublishDate = DateTime.Now.Date,
            GenreId = 1,
            AuthorId = 1
        };

        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        // arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = "Test Title",
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddYears(-2),
            GenreId = 1,
            AuthorId = 1
        };

        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeLessThanOrEqualTo(0);
    }
}
