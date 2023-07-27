using FluentValidation.TestHelper;
using WebApi.BookOperations;

namespace WebApi.UnitTests.Application.BookOperations.Queries;

public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    // Id 0 veya 0'dan küçük verilirse
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void WhenBookIdIsGivenZeroOrNegative_Validator_ShoulBeReturnError(int bookId)
    {
        // Arrange 
        var command = new GetBookDetailQuery(null, null) { BookId = bookId };

        // Act
        var validator = new GetBookDetailQueryValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BookId);
    }

    // Id 0'dan büyük verilirse
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void WhenBookIdIsGivenPositive_Validator_ShoulNotBeReturnError(int bookId)
    {
        // Arrange 
        var command = new GetBookDetailQuery(null, null) { BookId = bookId };

        // Act
        var validator = new GetBookDetailQueryValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.BookId);
    }
}
