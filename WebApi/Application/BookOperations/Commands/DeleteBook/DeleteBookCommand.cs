namespace WebApi.BookOperations;

public class DeleteBookCommand
{
    private readonly IBookStoreDbContext DbContext;
    public int BookId { get; set; }
    public DeleteBookCommand(IBookStoreDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public void Handle()
    {
        var book = DbContext.Books.SingleOrDefault(x => x.Id == BookId);
        if (book is null)
            throw new InvalidOperationException("Silinecek Kitap Bulunamadı");

        int authorId = book.AuthorId;

        DbContext.Books.Remove(book);
        DbContext.SaveChanges();

        // Kitaba ait AuthorId başka bir kitapta bulunmuyorsa Author.IsPublished özelliği false olarak değişmeli
        var authorBooks = DbContext.Books.Any(x => x.AuthorId == authorId && x.Id != BookId);
        var author = DbContext.Authors.FirstOrDefault(x => x.Id ==  authorId);

        if(author != null && !authorBooks)
        {
            author.IsPublished = false;
            DbContext.SaveChanges();
        }

    }
}
