﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations;

public class GetBooksQuery
{
    private readonly IBookStoreDbContext DbContext;
    private readonly IMapper Mapper;
    public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper) 
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = DbContext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.Id).ToList<Book>();
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
    public string Author { get; set; }

}
