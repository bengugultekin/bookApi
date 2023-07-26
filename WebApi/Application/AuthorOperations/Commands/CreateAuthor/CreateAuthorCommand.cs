using AutoMapper;

namespace WebApi.Application.AuthorOperations.Commands;

public class CreateAuthorCommand
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateAuthorViewModel model { get; set; }

    public CreateAuthorCommand(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.FirstName == model.FirstName && x.LastName == model.LastName);
        if (author is not null) 
        {
            throw new InvalidOperationException("Yazar Zaten Mevcut");
        }

        author = _mapper.Map<Author>(model);

        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();
    }
}

public class CreateAuthorViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}