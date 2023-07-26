using AutoMapper;

namespace WebApi.Application.AuthorOperations;

public class GetAuthorsQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<AuthorsViewModel> Handle()
    {
        var authors = _dbContext.Authors.Where(x => x.IsPublished).OrderBy(x => x.Id).ToList();
        List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authors);
        return vm;
    }
}

public class AuthorsViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsPublished { get; set; }
}

