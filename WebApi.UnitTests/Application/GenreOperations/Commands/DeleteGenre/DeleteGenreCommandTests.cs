using FluentAssertions;
using WebApi.Application.GenreOperations.Commands;
using WebApi.BookOperations;

namespace WebApi.UnitTests.Application.GenreOperations.Commands;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    // Silinecek genre bulunamadığında
    [Fact]
    public void WhenGenreDoesNotExist_InvalidOperationException_ShouldBeThrown()
    {
        // Arrange
        var invalidGenreId = 99;
        var command = new DeleteGenreCommand(_dbContext) { GenreId = invalidGenreId };

        // Act
        Action action = () => command.Handle();

        // Assert
        action.Should().Throw<InvalidOperationException>().WithMessage("Kitap Türü Bulunamadı");
    }

    // Happy test
    [Fact]
    public void WhenGenreExist_Genre_ShouldBeDeleted()
    {
        // Arrange
        var genreId = 10;
        var genre = new Genre { Id = genreId, Name = "Mystery"};
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        var command = new DeleteGenreCommand(_dbContext) { GenreId = genreId };

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        _dbContext.Genres.FirstOrDefault(x => x.Id == genre.Id).Should().BeNull();
    }
}
