using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands;

public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    // Yazar zaten mevcutsa
    [Fact]
    public void WhenAuthorAlreadyExists_Handle_ThrowsInvalidOperationException()
    {
        // Arrange
        var existingAuthor = new Author { Id = 10, FirstName = "J.R.R.", LastName = "Tolkien", DateOfBirth = new DateTime(1892, 1, 3) };
        _dbContext.Authors.Add(existingAuthor);
        _dbContext.SaveChanges();

        var command = new CreateAuthorCommand(_dbContext, _mapper)
        {
            model = new CreateAuthorViewModel
            {
                FirstName = existingAuthor.FirstName,
                LastName = existingAuthor.LastName,
                DateOfBirth = existingAuthor.DateOfBirth
            }
        };

        // Act & Assert
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Yazar Zaten Mevcut");
    }


    // Happy Test
    [Fact]
    public void WhenValidInputsAreGiven_AuthorShouldBeCreated()
    {
        // arrange
        CreateAuthorCommand command = new CreateAuthorCommand(_dbContext, _mapper);
        CreateAuthorViewModel Model = new CreateAuthorViewModel() {
            FirstName = "Isaac",
            LastName = "Asimov",
            DateOfBirth = new DateTime(1920, 1, 2)
        };

        command.model = Model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var createdAuthor = _dbContext.Authors.SingleOrDefault(x => x.FirstName == Model.FirstName && x.LastName == Model.LastName);
        createdAuthor.Should().NotBeNull();
        createdAuthor.FirstName.Should().Be(command.model.FirstName);
        createdAuthor.LastName.Should().Be(command.model.LastName);
        createdAuthor.DateOfBirth.Should().Be(command.model.DateOfBirth);
    }
}
