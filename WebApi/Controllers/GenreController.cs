using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands;
using WebApi.Application.GenreOperations.Queries;

namespace WebApi;

[ApiController]
[Route("[controller]s")]
public class GenreController : ControllerBase
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GenreController(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult GetGenres()
    {
        GetGenresQuery query = new GetGenresQuery(_context, _mapper);
        var obj = query.Handle();
        return Ok(obj);
    }

    [HttpGet("{id}")]
    public ActionResult GetGenreDetail(int id)
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
        query.GenreId = id;
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var obj = query.Handle();
        return Ok(obj);
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel model)
    {
        CreateGenreCommand command = new CreateGenreCommand(_context);
        command.Model = model;

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel model) 
    {
        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.GenreId = id;
        command.Model = model;

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGenre(int id)
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = id;

        DeleteGenreValidator validator = new DeleteGenreValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        return Ok();
    }
}
