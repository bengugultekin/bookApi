using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries;

public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    // Getirileck yazar id si bulunamadığında
    [Fact]
    public void WhenAuthorNotFound_Handle_ThrowsInvalidOperationException()
    {
        // Arrange
        var authorId = 99;
        var command = new GetAuthorDetailQuery(_dbContext, _mapper) { AuthorId = authorId };

        // Act & Assert
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().WithMessage("Yazar Bulunamadı");
    }

    // Happy test
    [Fact]
    public void WhenValidAuthorIdIsGiven_AuthorShouldBeReturned()
    {
        // Arrange
        var authorId = 15;
        var author = new Author { Id = authorId, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1980, 1, 1), IsPublished = true };
        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();

        var command = new GetAuthorDetailQuery(_dbContext, _mapper) { AuthorId = authorId };

        // Act
        var authorDetail = command.Handle();

        // Assert
        authorDetail.Should().NotBeNull();
        authorDetail.FirstName.Should().Be(author.FirstName);
        authorDetail.LastName.Should().Be(author.LastName);
        authorDetail.DateOfBirth.Should().Be(author.DateOfBirth);
        authorDetail.IsPublished.Should().Be(author.IsPublished);
    }
}
