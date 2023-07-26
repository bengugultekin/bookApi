using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations;

public class GetBooksQuery
{
    private readonly BookStoreDbContext DbContext;
    private readonly IMapper Mapper;
    public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper) 
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = DbContext.Books.Include(x => x.Genre).OrderBy(x => x.Id).ToList<Book>();
        List<BooksViewModel> vm = Mapper.Map<List<BooksViewModel>>(bookList);
        return vm;

    }
}

public class BooksViewModel
{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
    public string Genre { get; set; }

}
