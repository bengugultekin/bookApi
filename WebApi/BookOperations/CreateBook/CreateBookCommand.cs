namespace WebApi.BookOperations;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext DbContext;
    public CreateBookCommand(BookStoreDbContext dbContext) 
    {
        DbContext = dbContext;
    }

    public void Handle()
    {
        var book = DbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        if (book is not null)
            throw new InvalidOperationException("Kitap zaten mevcut");

        book = new Book();
        book.Title = Model.Title;
        book.PublishDate = Model.PublishDate;
        book.PageCount = Model.PageCount;
        book.GenreId = Model.GenreId;

        DbContext.Books.Add(book);
        DbContext.SaveChanges();
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
