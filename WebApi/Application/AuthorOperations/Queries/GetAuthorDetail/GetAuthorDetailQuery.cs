using AutoMapper;

namespace WebApi.Application.AuthorOperations.Queries;

public class GetAuthorDetailQuery
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public int AuthorId { get; set; }

    public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public AuthorDetailViewModel Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x =>x.IsPublished && x.Id == AuthorId);
        if(author is null) 
        {
            throw new InvalidOperationException("Yazar Bulunamadı");
        }
        AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);
        return vm;
    }
}

public class AuthorDetailViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsPublished { get; set; }
}
