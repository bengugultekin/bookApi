using AutoMapper;

namespace WebApi.Application.GenreOperations.Commands;

public class CreateGenreCommand
{
    public CreateGenreModel Model { get; set; }
    private readonly BookStoreDbContext _dbContext;

    public CreateGenreCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
        if(genre is not null)
        {
            throw new InvalidOperationException("Kitap Türü Zaten Mevcut");
        }
        genre = new Genre();
        genre.Name = Model.Name;
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();
    }
}

public class CreateGenreModel
{
    public string Name { get; set; }
}
