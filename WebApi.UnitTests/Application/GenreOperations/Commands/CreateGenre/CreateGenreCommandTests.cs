using FluentAssertions;
using WebApi.Application.GenreOperations.Commands;

namespace WebApi.UnitTests.Application.GenreOperations.Commands;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    // Mevcut Genre isminde kayıt eklenirse
    [Fact]
    public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange - hazırlık

        var genre = new Genre() { Name = "WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn"};
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();

        CreateGenreCommand command = new CreateGenreCommand(_dbContext);
        command.Model = new CreateGenreModel() { Name = genre.Name };

        // act - çalıştırma & assert - doğrulama

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Kitap Türü Zaten Mevcut");
    }

    // Happy Test
    [Fact]
    public void WhenValidGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        CreateGenreCommand command = new CreateGenreCommand(_dbContext);
        CreateGenreModel model = new CreateGenreModel() { Name = "Noval"};

        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // assert
        var genre = _dbContext.Genres.SingleOrDefault(genre => genre.Name == model.Name);
        genre.Should().NotBeNull();
    }
}
