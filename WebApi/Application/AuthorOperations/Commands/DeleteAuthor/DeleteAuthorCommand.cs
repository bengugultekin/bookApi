namespace WebApi.Application.AuthorOperations.Commands;

public class DeleteAuthorCommand
{
    private readonly IBookStoreDbContext _dbContext;
    public int AuthorId { get; set; }

    public DeleteAuthorCommand(IBookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);

        if (author is null)
        {
            throw new InvalidOperationException("Silinecek Yazar Bulunamadı");
        }
        else if (author.IsPublished == true)
        {
            throw new InvalidOperationException("Yazarın yayında olan kitapları bulunuyor. Önce kitapları silin.");
        }
        _dbContext.Authors.Remove(author);
        _dbContext.SaveChanges();
    }
}