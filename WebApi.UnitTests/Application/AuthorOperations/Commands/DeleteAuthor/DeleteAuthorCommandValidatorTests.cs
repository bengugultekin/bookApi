using FluentValidation.TestHelper;
using WebApi.Application.AuthorOperations.Commands;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands;

public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    // Geçersiz id girilirse
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidAuthorIdIsGiven_ValidationShouldFail(int authorId)
    {
        // Arrange
        var command = new DeleteAuthorCommand(null) { AuthorId = authorId };

        // Act
        var validator = new DeleteAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.AuthorId);
    }


    // Happy Test
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void WhenValidAuthorIdIsGiven_ValidationShouldPass(int authorId)
    {
        // Arrange
        var command = new DeleteAuthorCommand(null) { AuthorId = authorId };

        // Act
        var validator = new DeleteAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
