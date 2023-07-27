using FluentValidation.TestHelper;
using WebApi.BookOperations;

namespace WebApi.UnitTests.Application.BookOperations.Commands;

public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private DeleteBookCommandValidator _validator;

    public DeleteBookCommandValidatorTests()
    {
        _validator = new DeleteBookCommandValidator();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenBookIdEqualsToZeroOrNegative_Validator_ShouldBeReturnError(int bookId)
    {
        // Arrange
        var command = new DeleteBookCommand(null) { BookId = bookId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(command => command.BookId);
    }


    // Happy test
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void WhenBookIdIsPositive_Validator_ShoulNotBeReturnError(int bookId)
    {
        // Arrange
        var command = new DeleteBookCommand(null) { BookId = bookId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(command => command.BookId);
    }
}
