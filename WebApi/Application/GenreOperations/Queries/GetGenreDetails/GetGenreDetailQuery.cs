using AutoMapper;

namespace WebApi.Application.GenreOperations.Queries;

public class GetGenreDetailQuery
{
    public int GenreId { get; set; }
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenreDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x => x.isActive && x.Id == GenreId);
        if(genre is  null) 
        {
            throw new InvalidOperationException("Kitap türü bulunamadı");
        }
        GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(genre);
        return vm;
    }
}

public class GenreDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isActive { get; set; }
}