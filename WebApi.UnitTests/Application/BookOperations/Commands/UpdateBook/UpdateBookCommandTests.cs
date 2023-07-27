using AutoMapper;
using FluentAssertions;
using WebApi.BookOperations;
using static WebApi.BookOperations.UpdateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;

    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    //Happy Test - Güncellenecek Kitap Id si Mevcut İse
    [Fact]
    public void WhenBookExist_Book_ShouldBeUpdated()
    {
        // Arrange
        var authorId = 1;
        var bookId = 10;
        var existBook = new Book { Id = bookId, Title = "WhenBookExist_Book_ShouldBeUpdated", PageCount = 200, PublishDate = DateTime.Now.Date.AddYears(-5), GenreId = 2, AuthorId = authorId };
        _dbContext.Books.Add(existBook);
        _dbContext.SaveChanges();

        var command = new UpdateBookCommand(_dbContext) { BookId = bookId };
        command.Model = new UpdateBookModel()
        {
            Title = "Updated Book",
            GenreId = 3,
            AuthorId = authorId
        };

        // Act
        command.Handle();

        // Assert
        var updatedBook = _dbContext.Books.Find(bookId);
        updatedBook.Should().NotBeNull();
        updatedBook.Title.Should().Be(command.Model.Title);
        updatedBook.GenreId.Should().Be(command.Model.GenreId);

    }

    // Güncellenecek Id Bulunamadığında
    [Fact]
    public void WhenBookDoesNotExist_InvalidOperationException_ShouldBeThrown()
    {
        // Arrange
        var invalidBookId = 99;
        var command = new UpdateBookCommand(_dbContext) { BookId = invalidBookId };

        command.Model = new UpdateBookModel()
        {
            Title = "Updated Book",
            GenreId = 1,
            AuthorId = 1
        };

        // Act
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Güncellenecek Kitap Bulunamadı");
    }

}

