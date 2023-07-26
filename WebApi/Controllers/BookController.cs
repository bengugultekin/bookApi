using AutoMapper;
using FluentValidation;
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
        GetBookDetailQuery query = new GetBookDetailQuery(dbContext, Mapper);
        query.BookId = id;
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        validator.ValidateAndThrow(query);
        result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(dbContext, Mapper);
        command.Model = newBook;
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }


    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        UpdateBookCommand command = new UpdateBookCommand(dbContext);
        command.BookId = id;
        command.Model = updatedBook;
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id) 
    {
        DeleteBookCommand command = new DeleteBookCommand(dbContext);
        command.BookId = id;
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok(); 
    }

}
