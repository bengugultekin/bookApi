using FluentValidation.TestHelper;
using WebApi.BookOperations;

namespace WebApi.UnitTests.Application.BookOperations.Commands;

public class UpdateBookCommanValidatorTests : IClassFixture<CommonTestFixture>
{
    // Girilen kitap id si 0 dan büyük değilse
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenBookIdIsInvalid_Validator_ShoulBeReturnError(int bookId)
    {
        // Arrange 
        var command = new UpdateBookCommand(null) { BookId = bookId, Model = new UpdateBookCommand.UpdateBookModel() };

        // Act
        var validator = new UpdateBookCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BookId);
    }

    // Girilen Id 0 dan büyükse - Happy Test
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void WhenBookIdIsValid_Validator_ShouldNotBeReturnError(int bookId)
    {
        // Arrange 
        var command = new UpdateBookCommand(null) { BookId = bookId, Model = new UpdateBookCommand.UpdateBookModel() };

        // Act
        var validator = new UpdateBookCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.BookId);
    }

    // Genre Id geçerli olduğunda
    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public void WhenGenreIdIsValid_Validator_ShouldPass(int genreId)
    {
        // Arrange
        var command = new UpdateBookCommand(null) { Model = new UpdateBookCommand.UpdateBookModel { GenreId = genreId } };

        // Act
        var validator = new UpdateBookCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Model.GenreId);
    }

    // Genre Id geçersiz olduğunda

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenGenreIdIsInvalid_Validator_ShouldFail(int genreId)
    {
        // Arrange
        var command = new UpdateBookCommand(null) { Model = new UpdateBookCommand.UpdateBookModel { GenreId = genreId } };

        // Act
        var validator = new UpdateBookCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Model.GenreId);
    }

    // Author Id Geçerli Olduğunda
    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public void WhenAuthorIdIsValid_Validator_ShouldPass(int authorId)
    {
        // Arrange
        var command = new UpdateBookCommand(null) { Model = new UpdateBookCommand.UpdateBookModel { AuthorId = authorId } };

        // Act
        var validator = new UpdateBookCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Model.AuthorId);
    }

    // Author Id Geçersiz Olduğunda
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenAuthorIdIsInvalid_Validator_ShouldFail(int authorId)
    {
        // Arrange
        var command = new UpdateBookCommand(null) { Model = new UpdateBookCommand.UpdateBookModel { AuthorId = authorId } };

        // Act
        var validator = new UpdateBookCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Model.AuthorId);
    }

    // Title Geçerli olduğunda
    [Theory]
    [InlineData("Lord of the Rings")]
    [InlineData("Harry Potter")]
    public void WhenTitleIsValid_Validator_ShouldPass(string title)
    {
        // Arrange
        var command = new UpdateBookCommand(null) { Model = new UpdateBookCommand.UpdateBookModel { Title = title } };

        // Act
        var validator = new UpdateBookCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Model.Title);
    }

    //Title Geçersiz Olduğunda
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("Ab")]
    [InlineData("123")]
    public void WhenTitleIsInvalid_Validator_ShouldFail(string title)
    {
        // Arrange
        var command = new UpdateBookCommand(null) { Model = new UpdateBookCommand.UpdateBookModel { Title = title } };

        // Act
        var validator = new UpdateBookCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Model.Title);
    }

}
