using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries;
using WebApi.BookOperations;

namespace WebApi.UnitTests.Application.GenreOperations.Queries;

public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    // Geçerli genre id si verilirse - Happy Test
    [Fact]
    public void WhenValidGenreIdIsGiven_Genre_ShouldBeReturned()
    {
        // Arrange
        var genreId = 1;

        var query = new GetGenreDetailQuery(_dbContext, _mapper) { GenreId = genreId };

        // Act
        var result = query.Handle();

        // Assert
        result.Should().NotBeNull();
    }

    // Geçersiz genre id si verilirse
    [Fact]
    public void WhenInvalidGenreIdIsGiven_Genre_ShouldNotBeReturned()
    {
        // Arrange
        var genreId = 99;

        var query = new GetGenreDetailQuery(_dbContext, _mapper) { GenreId = genreId };

        // Act & Assert
        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Kitap türü bulunamadı");
    }
}
