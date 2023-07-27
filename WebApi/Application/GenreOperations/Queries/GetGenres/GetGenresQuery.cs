using AutoMapper;

namespace WebApi.Application.GenreOperations.Queries;

public class GetGenresQuery
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenresQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<GenresViewModel> Handle()
    {
        var genres = _dbContext.Genres.Where(x => x.isActive).OrderBy(x => x.Id);
        List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genres);
        return vm;
    }
}

public class GenresViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isActive { get; set; }
}