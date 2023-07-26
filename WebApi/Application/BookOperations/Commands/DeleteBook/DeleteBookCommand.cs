namespace WebApi.BookOperations;

public class DeleteBookCommand
{
    private readonly BookStoreDbContext DbContext;
    public int BookId { get; set; }
    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public void Handle()
    {
        var book = DbContext.Books.SingleOrDefault(x => x.Id == BookId);
        if (book is null)
            throw new InvalidOperationException("Silinecek Kitap Bulunamadı");

        DbContext.Books.Remove(book);
        DbContext.SaveChanges();
    }
}
