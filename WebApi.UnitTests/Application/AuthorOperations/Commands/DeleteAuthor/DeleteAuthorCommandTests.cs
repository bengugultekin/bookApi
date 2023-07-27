using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    // Silinecek yazar id si bulunamadığında
    [Fact]
    public void WhenAuthorNotFound_Handle_ThrowsInvalidOperationException()
    {
        // Arrange
        var authorId = 10;
        var command = new DeleteAuthorCommand(_dbContext) { AuthorId = authorId };

        // Act & Assert
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Silinecek Yazar Bulunamadı");
    }

    // Yazarın yayında olan kitapları varsa
    [Fact]
    public void WhenAuthorHasPublishedBooks_Handle_ThrowsInvalidOperationException()
    {
        // Arrange
        var authorId = 13;
        var author = new Author { Id = authorId, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1980, 1, 1), IsPublished = true };
        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();

        var command = new DeleteAuthorCommand(_dbContext) { AuthorId = authorId };

        // Act & Assert
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Yazarın yayında olan kitapları bulunuyor. Önce kitapları silin.");
    }

    // Happy test
    [Fact]
    public void WhenValidAuthorIdIsGiven_AuthorShouldBeDeleted()
    {
        // Arrange
        var authorId = 12;
        var author = new Author { Id = authorId, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1980, 1, 1), IsPublished = false };
        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();

        var command = new DeleteAuthorCommand(_dbContext) { AuthorId = authorId };

        // Act
        command.Handle();

        // Assert
        var deletedAuthor = _dbContext.Authors.Find(authorId);
        deletedAuthor.Should().BeNull();
    }
}
