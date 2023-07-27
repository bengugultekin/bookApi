using AutoMapper;
using FluentAssertions;
using WebApi.BookOperations;

namespace WebApi.UnitTests.Application.BookOperations.Queries;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    // Geçerli kitap id si verilirse
    [Fact]
    public void WhenValidBookIdIsGiven_Book_ShouldBeReturned()
    {
        // Arrange
        var bookId = 1;

        var query = new GetBookDetailQuery(_dbContext, _mapper) { BookId = bookId };

        // Act
        var result = query.Handle();

        // Assert
        result.Should().NotBeNull();
    }

    // Geçersiz kitap id si verilirse
    [Fact]
    public void WhenInvalidBookIdIsGiven_Book_ShouldNotBeReturned()
    {
        // Arrange
        var bookId = 99;

        var query = new GetBookDetailQuery(_dbContext, _mapper) { BookId = bookId };

        // Act & Assert
        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Kitap bulunamadı");
    }
}
