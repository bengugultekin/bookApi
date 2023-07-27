using FluentValidation.TestHelper;
using WebApi.Application.AuthorOperations.Commands;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands;

public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    // Geçersiz İsim Girilirse
    [Theory]
    [InlineData("J.R")]
    [InlineData("A.")]
    public void WhenFirstNameIsLessThan4Characters_Validator_ShouldFail(string firstName)
    {
        // Arrange
        var command = new UpdateAuthorCommand(null) { model = new UpdateAuthorViewModel { FirstName = firstName } };

        // Act
        var validator = new UpdateAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.model.FirstName);
    }

    // Geçersiz Soyisim girilirse
    [Theory]
    [InlineData("Tol")]
    [InlineData("As")]
    public void WhenLastNameIsLessThan4Characters_Validator_ShouldFail(string lastName)
    {
        // Arrange
        var command = new UpdateAuthorCommand(null) { model = new UpdateAuthorViewModel { LastName = lastName } };

        // Act
        var validator = new UpdateAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.model.LastName);
    }

    // Doğum tarihi boş girilirse
    [Fact]
    public void WhenDateOfBirthIsEmpty_Validator_ShouldFail()
    {
        // Arrange
        var command = new UpdateAuthorCommand(null) { model = new UpdateAuthorViewModel { DateOfBirth = default } };

        // Act
        var validator = new UpdateAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.model.DateOfBirth);
    }

    // Geçersiz doğum tarihi girilirse
    [Fact]
    public void WhenDateOfBirthIsGreaterThanToday_Validator_ShouldFail()
    {
        // Arrange
        var command = new UpdateAuthorCommand(null) { model = new UpdateAuthorViewModel { DateOfBirth = DateTime.Now.Date.AddDays(1) } };

        // Act
        var validator = new UpdateAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.model.DateOfBirth);
    }

    // Happy Test
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldPass()
    {
        // Arrange
        var command = new UpdateAuthorCommand(null) { model = new UpdateAuthorViewModel { FirstName = "Isaac", LastName = "Asimov", DateOfBirth = new DateTime(1920, 1, 2) } };

        // Act
        var validator = new UpdateAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
