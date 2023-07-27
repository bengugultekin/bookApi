using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.UnitTests;

public class CommonTestFixture
{
    public BookStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDbContext").Options;
        Context = new BookStoreDbContext(options);
        Context.Database.EnsureCreated();
        Context.AddAuthors();
        Context.AddBooks();
        Context.AddGenres();
        Context.SaveChanges();

        Mapper = new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); }).CreateMapper();
    }
}
