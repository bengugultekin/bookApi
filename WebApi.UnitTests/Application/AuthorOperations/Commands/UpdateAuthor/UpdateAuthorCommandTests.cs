using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    // Yazar Id si bulunamadıysa
    [Fact]
    public void WhenAuthorNotFound_Handle_ThrowsInvalidOperationException()
    {
        // Arrange
        var authorId = 10;
        var command = new UpdateAuthorCommand(_dbContext) { AuthorId = authorId, model = new UpdateAuthorViewModel() };

        // Act & Assert
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Yazar Bulunamadı");
    }

    // Yazar Id si mevcutsa - Happy Test
    [Fact]
    public void WhenValidInputsAreGiven_AuthorShouldBeUpdated()
    {
        // Arrange
        var authorId = 15;
        var author = new Author { Id = authorId, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1980, 1, 1), IsPublished = false };
        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();

        var newFirstName = "Jane";
        var newLastName = "Smith";
        var newDateOfBirth = new DateTime(1990, 2, 2);
        var newIsPublished = true;

        var command = new UpdateAuthorCommand(_dbContext) { AuthorId = authorId, model = new UpdateAuthorViewModel { FirstName = newFirstName, LastName = newLastName, DateOfBirth = newDateOfBirth, IsPublished = newIsPublished } };

        // Act
        command.Handle();

        // Assert
        var updatedAuthor = _dbContext.Authors.SingleOrDefault(x => x.Id == authorId);
        updatedAuthor.Should().NotBeNull();
        updatedAuthor.FirstName.Should().Be(newFirstName);
        updatedAuthor.LastName.Should().Be(newLastName);
        updatedAuthor.DateOfBirth.Should().Be(newDateOfBirth);
        updatedAuthor.IsPublished.Should().Be(newIsPublished);
    }
}
