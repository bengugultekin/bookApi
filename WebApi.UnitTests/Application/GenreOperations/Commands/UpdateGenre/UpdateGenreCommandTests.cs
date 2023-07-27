using FluentAssertions;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.UnitTests.Application.GenreOperations.Commands;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    //Happy Test - Güncellenecek Genre Id si Mevcut İse
    [Fact]
    public void WhenGenreIdExist_Genre_ShouldBeUpdated()
    {
        // Arrange
        var genreId = 10;
        var existGenre = new Genre { Id = genreId, Name = "Drama" };
        _dbContext.Genres.Add(existGenre);
        _dbContext.SaveChanges();

        var command = new UpdateGenreCommand(_dbContext) { GenreId = genreId };
        command.Model = new UpdateGenreModel()
        {
            Name = "Updated Name",
        };

        // Act
        command.Handle();

        // Assert
        var updatedGenre = _dbContext.Genres.Find(genreId);
        updatedGenre.Should().NotBeNull();
        updatedGenre.Name.Should().Be(command.Model.Name);

    }

    // Güncellenecek Id Bulunamadığında
    [Fact]
    public void WhenGenreDoesNotExist_InvalidOperationException_ShouldBeThrown()
    {
        // Arrange
        var invalidGenreId = 99;
        var command = new UpdateGenreCommand(_dbContext) { GenreId = invalidGenreId };

        command.Model = new UpdateGenreModel()
        {
            Name = "Updated Genre"
        };

        // Act
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Kitap Türü Bulunamadı");
    }

    // Güncellenecek kitap ismi zaten mevcutsa
    [Fact]
    public void WhenGenreNameAlreadyExist_Genre_ThrowsInvalidOperationException()
    {
        // Arrange
        var genreId = 5;
        var genreName = "Science Fiction";
        var genre = new Genre { Id = genreId, Name = genreName };
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        var command = new UpdateGenreCommand(_dbContext) { GenreId = genreId, Model = new UpdateGenreModel { Name = genreName } };

        // Act & Assert
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Aynı İsimli Bir Kitap Türü Zaten Mevcut");
    }
}
