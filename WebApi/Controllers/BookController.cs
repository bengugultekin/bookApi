using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
    private readonly IMapper Mapper;

    public BookController (BookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        Mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks() 
    {
        GetBooksQuery query = new GetBooksQuery(dbContext, Mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        BookDetailViewModel result;
        try
        {
            GetBookDetailQuery query = new GetBookDetailQuery(dbContext, Mapper);
            query.BookId = id;
            result = query.Handle();
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(dbContext, Mapper);
        try 
        {
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
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
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch(Exception ex) 
        {
            return BadRequest(ex.Message);
        }
        return Ok(); 
    }

}
