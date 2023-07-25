using AutoMapper;

namespace WebApi.BookOperations;

public class GetBookDetailQuery
{
    private readonly BookStoreDbContext DbContext;
    private readonly IMapper Mapper;
    public int BookId { get; set; }

    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = DbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
        if (book is null) 
        {
            throw new InvalidOperationException("Kitap bulunamadı");
        }
        BookDetailViewModel vm = Mapper.Map<BookDetailViewModel>(book);
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