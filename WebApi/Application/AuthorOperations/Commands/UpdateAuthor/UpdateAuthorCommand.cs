namespace WebApi.Application.AuthorOperations.Commands;

public class UpdateAuthorCommand
{
    private readonly BookStoreDbContext _dbContext;
    public int AuthorId { get; set; }
    public UpdateAuthorViewModel model { get; set; }

    public UpdateAuthorCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if(author is null) 
        {
            throw new InvalidOperationException("Yazar Bulunamadı");
        }
        author.FirstName = string.IsNullOrEmpty(model.FirstName.Trim()) ? author.FirstName : model.FirstName;
        author.LastName = string.IsNullOrEmpty(model.LastName.Trim()) ? author.LastName: model.LastName;
        author.DateOfBirth = model.DateOfBirth;
        author.IsPublished = model.IsPublished;

        _dbContext.SaveChanges();
    }
}
public class UpdateAuthorViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsPublished { get; set; } = false;
}