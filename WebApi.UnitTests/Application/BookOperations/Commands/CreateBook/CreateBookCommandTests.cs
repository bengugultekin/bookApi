using AutoMapper;
using FluentAssertions;
using WebApi.BookOperations;
using static WebApi.BookOperations.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands;

public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange - hazırlık

        var book = new Book() { Title= "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount=100, PublishDate = new DateTime(1990, 01, 10), GenreId=1, AuthorId=1 };
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();

        CreateBookCommand command = new CreateBookCommand(_dbContext, _mapper);
        command.Model = new CreateBookModel() { Title = book.Title };

        // act - çalıştırma & assert - doğrulama

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Kitap zaten mevcut");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        // arrange
        CreateBookCommand command = new CreateBookCommand( _dbContext, _mapper);
        CreateBookModel model = new CreateBookModel() { Title = "Hobbit", PageCount = 1000, PublishDate = DateTime.Now.Date.AddYears(-10), GenreId = 2, AuthorId = 1 };

        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // assert
        var book = _dbContext.Books.SingleOrDefault(book => book.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.AuthorId.Should().Be(model.AuthorId);
        book.GenreId.Should().Be(model.GenreId);
    }

}
