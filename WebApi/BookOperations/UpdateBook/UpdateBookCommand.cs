namespace WebApi.BookOperations;

public class UpdateBookCommand
{
    private readonly BookStoreDbContext DbContext;
    public int BookId { get; set; }
    public UpdateBookModel Model {get; set; }
    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public void Handle()
    {
        var book = DbContext.Books.SingleOrDefault(x => x.Id == BookId);

        if (book is null)
            throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı");

        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.Title = Model.Title != default ? Model.Title : book.Title;
        DbContext.SaveChanges();
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}
