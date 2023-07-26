using AutoMapper;
using WebApi.Application.AuthorOperations;
using WebApi.Application.AuthorOperations.Commands;
using WebApi.Application.AuthorOperations.Queries;
using WebApi.Application.GenreOperations.Queries;
using WebApi.BookOperations;
using static WebApi.BookOperations.CreateBookCommand;

namespace WebApi;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
        CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

        CreateMap<Genre, GenresViewModel>();
        CreateMap<Genre, GenreDetailViewModel>();

        CreateMap<Author, AuthorsViewModel>();
        CreateMap<Author, AuthorDetailViewModel>();
        CreateMap<CreateAuthorViewModel, Author>();
        CreateMap<UpdateAuthorViewModel, Author>();
    }
}
