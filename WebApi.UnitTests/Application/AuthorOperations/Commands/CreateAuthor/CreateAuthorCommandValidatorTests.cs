using FluentValidation.TestHelper;
using WebApi.Application.AuthorOperations.Commands;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands;

public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    // Geçersiz İsim Girilirse
    [Theory]
    [InlineData("J.R")]
    [InlineData("A.")]
    public void WhenFirstNameIsLessThan4Characters_Validator_ShouldFail(string firstName)
    {
        // Arrange
        var command = new CreateAuthorCommand(null, null) { model = new CreateAuthorViewModel { FirstName = firstName } };

        // Act
        var validator = new CreateAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.model.FirstName);
    }

    // Geçersiz Soyisim Girilirse
    [Theory]
    [InlineData("Tol")]
    [InlineData("As")]
    public void WhenLastNameIsLessThan4Characters_Validator_ShouldFail(string lastName)
    {
        // Arrange
        var command = new CreateAuthorCommand(null, null) { model = new CreateAuthorViewModel { LastName = lastName } };

        // Act
        var validator = new CreateAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.model.LastName);
    }

    // Doğum tarihi boş girilirse
    [Fact]
    public void WhenDateOfBirthIsEmpty_Validator_ShouldFail()
    {
        // Arrange
        var command = new CreateAuthorCommand(null, null) { model = new CreateAuthorViewModel { DateOfBirth = default } };

        // Act
        var validator = new CreateAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.model.DateOfBirth);
    }

    // Geçersiz doğum tarihi girilirse
    [Fact]
    public void WhenDateOfBirthIsGreaterThanToday_Validator_ShouldFail()
    {
        // Arrange
        var command = new CreateAuthorCommand(null, null) { model = new CreateAuthorViewModel { DateOfBirth = DateTime.Now.Date.AddDays(1) } };

        // Act
        var validator = new CreateAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.model.DateOfBirth);
    }

    // Inputlar Doğru Girilirse - Happy Test
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldPass()
    {
        // Arrange
        var command = new CreateAuthorCommand(null, null) { model = new CreateAuthorViewModel { FirstName = "Isaac", LastName = "Asimov", DateOfBirth = new DateTime(1920, 1, 2) } };

        // Act
        var validator = new CreateAuthorCommandValidator();
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
