using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations;
using static WebApi.BookOperations.CreateBookCommand;

namespace WebApi;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{

    private readonly BookStoreDbContext dbContext;

    public BookController (BookStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetBooks() 
    {
        GetBooksQuery query = new GetBooksQuery(dbContext);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        var book = dbContext.Books.Where(book => book.Id == id).SingleOrDefault();
        return book;
    }

    //[HttpGet]
    //public Book Get([FromQuery] string id)
    //{
    //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
    //    return book;
    //}

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(dbContext);
        try 
        {
            command.Model = newBook;
            command.Handle();
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }      
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
    {
        var book = dbContext.Books.SingleOrDefault(x => x.Id == id);

        if(book is null)
            return BadRequest();

        book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

        dbContext.SaveChanges();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id) 
    {  
        var book = dbContext.Books.SingleOrDefault(x => x.Id==id);
        if (book is null)
            return BadRequest();

        dbContext.Books.Remove(book);
        dbContext.SaveChanges();
        return Ok(); 
    }

}
