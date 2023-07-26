using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations;
using WebApi.Application.AuthorOperations.Commands;
using WebApi.Application.GenreOperations.Queries;

namespace WebApi;

[ApiController]
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public AuthorController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
        var obj = query.Handle;
        return Ok(obj);
    }

    [HttpGet("{id}")]
    public ActionResult GetGenreDetail(int id)
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = id;
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var obj = query.Handle;
        return Ok(obj);
    }

    [HttpPost]
    public IActionResult AddAuthor([FromBody] CreateAuthorViewModel model)
    {
        CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
        command.model = model;

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorViewModel model)
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        command.AuthorId = id;
        command.model = model;

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id) 
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = id;

        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
}
