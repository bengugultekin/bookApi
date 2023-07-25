namespace WebApi.BookOperations;

public class GetBookDetailQuery
{
    private readonly BookStoreDbContext DbContext;
    public int BookId { get; set; }

    public GetBookDetailQuery(BookStoreDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public BookDetailViewModel Handle()
    {
        var book = DbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
        if (book is null) 
        {
            throw new InvalidOperationException("Kitap bulunamadı");
        }
        BookDetailViewModel vm = new BookDetailViewModel();
        vm.Title = book.Title;
        vm.PageCount = book.PageCount;
        vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
        vm.Genre = ((GenreEnum)book.GenreId).ToString();
        return vm;
    }
}

public class BookDetailViewModel
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
}