using AutoMapper;
using WebApi.BookOperations;
using static WebApi.BookOperations.CreateBookCommand;

namespace WebApi;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
    }
}
