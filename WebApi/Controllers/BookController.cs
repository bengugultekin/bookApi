using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations;
using static WebApi.BookOperations.CreateBookCommand;
using static WebApi.BookOperations.UpdateBookCommand;

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
    public IActionResult GetById(int id)
    {
        BookDetailViewModel result;
        try
        {
            GetBookDetailQuery query = new GetBookDetailQuery(dbContext);
            query.BookId = id;
            result = query.Handle();
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }

        return Ok(result);
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
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        try
        {
            UpdateBookCommand command = new UpdateBookCommand(dbContext);
            command.BookId = id;
            command.Model = updatedBook;
            command.Handle();
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id) 
    {  
        try
        {
            DeleteBookCommand command = new DeleteBookCommand(dbContext);
            command.BookId = id;
            command.Handle();
        }
        catch(Exception ex) 
        {
            return BadRequest(ex.Message);
        }
        return Ok(); 
    }

}
