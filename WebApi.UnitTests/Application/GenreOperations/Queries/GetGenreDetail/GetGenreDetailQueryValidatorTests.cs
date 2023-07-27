using FluentValidation.TestHelper;
using WebApi.Application.GenreOperations.Queries;

namespace WebApi.UnitTests.Application.GenreOperations.Queries;

public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    // Geçersiz Genre Id Girilirse
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenGenreIdIsInvalid_Validator_ShouldFail(int genreId)
    {
        // Arrange
        var query = new GetGenreDetailQuery(null, null) { GenreId = genreId };

        // Act
        var validator = new GetGenreDetailQueryValidator();
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.GenreId);
    }

    // Happy Test
    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    public void WhenGenreIdIsValid_Validator_ShouldPass(int genreId)
    {
        // Arrange
        var query = new GetGenreDetailQuery(null, null) { GenreId = genreId };

        // Act
        var validator = new GetGenreDetailQueryValidator();
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.GenreId);
    }
}
