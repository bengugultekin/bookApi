namespace WebApi.BookOperations;

public class UpdateBookCommand
{
    private readonly BookStoreDbContext DbContext;
    public int BookId { get; set; }
    public int AuthorId { get; set; }
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

        int oldAuthorId = book.AuthorId;

        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.AuthorId = Model.AuthotId != default ? Model.AuthotId : book.AuthorId;
        book.Title = Model.Title != default ? Model.Title : book.Title;
        DbContext.SaveChanges();

        // Yazar bilgisi güncellendiğinde, eğer yazarın başka kitabı kalmamışsa IsPublished false olmalı
        var authorBooks = DbContext.Books.Any(x => x.AuthorId == oldAuthorId && x.Id != BookId);
        var author = DbContext.Authors.FirstOrDefault(x => x.Id == oldAuthorId);

        if(author != null && !authorBooks)
        {
            author.IsPublished = false;
            DbContext.SaveChanges();
        }

        // Güncellenen yazarın IsPublished özelliği true yapalım
        var updatedAuthor = DbContext.Authors.FirstOrDefault(x => x.Id == book.AuthorId);
        if (updatedAuthor != null)
        {
            updatedAuthor.IsPublished = true;
            DbContext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthotId { get; set; }
    }
}
