namespace WebApi.Application.GenreOperations.Commands;

public class UpdateGenreCommand
{
    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }

    private readonly BookStoreDbContext _dbContext;
    public UpdateGenreCommand (BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x=> x.Id == GenreId);
        if (genre is null) 
        {
            throw new InvalidOperationException("Kitap Türü Bulunamadı");
        }
        if(_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
        {
            throw new InvalidOperationException("Aynı İsimli Bir Kitap Türü Zaten Mevcut");
        }

        genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
        genre.isActive = Model.isActive;
        _dbContext.SaveChanges();
    }

}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool isActive { get; set; } = true;
}
