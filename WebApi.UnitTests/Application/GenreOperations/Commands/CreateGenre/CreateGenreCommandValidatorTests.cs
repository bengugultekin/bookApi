using FluentValidation.TestHelper;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.UnitTests.Application.GenreOperations.Commands;

public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    // Geçersiz Genre İsmi Verilirse
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc")]
    public void WhenNameIsInvalid_Validator_ShouldFail(string name)
    {
        // Arrange
        var command = new CreateGenreCommand(null) { Model = new CreateGenreModel { Name = name } };

        // Act
        var validator = new CreateGenreCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Model.Name);
    }

    // Happy Test
    [Theory]
    [InlineData("Fantasy")]
    [InlineData("Science Fiction")]
    public void WhenNameIsValid_Validator_ShouldPass(string name)
    {
        // Arrange
        var command = new CreateGenreCommand(null) { Model = new CreateGenreModel { Name = name } };

        // Act
        var validator = new CreateGenreCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Model.Name);
    }
}
