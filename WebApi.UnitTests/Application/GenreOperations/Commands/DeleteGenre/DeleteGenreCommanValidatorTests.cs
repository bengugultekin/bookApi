using FluentValidation.TestHelper;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.UnitTests.Application.GenreOperations.Commands;

public class DeleteGenreCommanValidatorTests : IClassFixture<CommonTestFixture>
{
    // Genre Id 0 veya 0'dan küçük verilirse
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenGenreIdIsInvalid_ValidationShouldFail(int genreId)
    {
        // Arrange
        var command = new DeleteGenreCommand(null) { GenreId = genreId };

        // Act
        var validator = new DeleteGenreValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.GenreId);
    }

    // Happy Test
    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public void WhenGenreIdIsValid_ValidationShouldPass(int genreId)
    {
        // Arrange
        var command = new DeleteGenreCommand(null) { GenreId = genreId };

        // Act
        var validator = new DeleteGenreValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.GenreId);
    }
}
