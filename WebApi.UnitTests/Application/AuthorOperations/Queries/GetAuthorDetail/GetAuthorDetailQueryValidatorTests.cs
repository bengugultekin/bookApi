using FluentValidation.TestHelper;
using WebApi.Application.AuthorOperations.Queries;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries;

public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    // Geçersiz id girildiğinde
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenInvalidAuthorIdIsGiven_Validator_ShouldFail(int authorId)
    {
        // Arrange
        var query = new GetAuthorDetailQuery(null, null) { AuthorId = authorId };

        // Act
        var validator = new GetAuthorDetailQueryValidator();
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.AuthorId);
    }

    //Happy test
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void WhenValidAuthorIdIsGiven_Validator_ShouldPass(int authorId)
    {
        // Arrange
        var query = new GetAuthorDetailQuery(null, null) { AuthorId = authorId };

        // Act
        var validator = new GetAuthorDetailQueryValidator();
        var result = validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
