using FluentAssertions;
using WebApi.BookOperations;

namespace WebApi.UnitTests.Application.BookOperations.Commands;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    // Silinecek kitap bulunamadığında
    [Fact]
    public void WhenBookDoesNotExist_InvalidOperationException_ShouldBeThrown()
    {
        // Arrange
        var invalidBookId = 99;
        var command = new DeleteBookCommand(_dbContext) { BookId = invalidBookId };

        // Act
        Action action = () => command.Handle();

        // Assert
        action.Should().Throw<InvalidOperationException>().WithMessage("Silinecek Kitap Bulunamadı");
    }

    // Happy test
    [Fact]
    public void WhenBookExist_Book_ShouldBeDeleted()
    {
        // Arrange
        var authorId = 3;
        var bookId = 10;
        var book = new Book { Id = bookId, Title = "WhenBookExist_Book_ShouldBeDeleted", AuthorId = authorId, PageCount = 100, GenreId = 1, PublishDate = new DateTime(1990, 01, 10) };
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();

        var command = new DeleteBookCommand(_dbContext) { BookId = book.Id };

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        _dbContext.Books.FirstOrDefault(x => x.Id == book.Id).Should().BeNull();
        _dbContext.Authors.FirstOrDefault(x => x.Id == authorId)?.IsPublished.Should().BeFalse();
    }
}
