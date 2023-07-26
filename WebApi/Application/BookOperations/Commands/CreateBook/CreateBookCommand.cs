using AutoMapper;

namespace WebApi.BookOperations;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext DbContext;
    private readonly IMapper Mapper;
    public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper) 
    {
        DbContext = dbContext;
        Mapper = mapper;
    }

    public void Handle()
    {
        var book = DbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        if (book is not null)
            throw new InvalidOperationException("Kitap zaten mevcut");

        book = Mapper.Map<Book>(Model);

        DbContext.Books.Add(book);
        DbContext.SaveChanges();

        // Yeni bir kitap eklendiğinde, yazarın IsPublished özelliği true olmalı
        var author = DbContext.Authors.FirstOrDefault(x => x.Id == Model.AuthorId);
        if(author != null) 
        {
            author.IsPublished = true;
            DbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
