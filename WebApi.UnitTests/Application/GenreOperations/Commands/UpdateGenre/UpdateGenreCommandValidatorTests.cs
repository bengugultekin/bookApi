using FluentValidation.TestHelper;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.UnitTests.Application.GenreOperations.Commands;

public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    // Geçersiz Genre Name girilirse
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc")]
    public void WhenNameIsEmptyOrLessThan4Characters_Validator_ShouldPass(string name)
    {
        // Arrange
        var command = new UpdateGenreCommand(null) { Model = new UpdateGenreModel { Name = name } };

        // Act
        var validator = new UpdateGenreCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Model.Name);
    }

    // Happy Test
    [Theory]
    [InlineData("Fant")]
    [InlineData("Science Fiction")]
    public void WhenNameIsNotEmptyAndAtLeast4Characters_Validator_ShouldPass(string name)
    {
        // Arrange
        var command = new UpdateGenreCommand(null) { Model = new UpdateGenreModel { Name = name } };

        // Act
        var validator = new UpdateGenreCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Model.Name);
    }
}
