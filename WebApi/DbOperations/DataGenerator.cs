using Microsoft.EntityFrameworkCore;

namespace WebApi;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if(context.Books.Any()) 
            {
                return;
            }
            context.Authors.AddRange(
                new Author 
                {
                    FirstName = "Victor",
                    LastName = "Hugo",
                    DateOfBirth = new DateTime(1802, 02, 26),
                    IsPublished = true
                },
                new Author
                {
                    FirstName = "William",
                    LastName = "Shakespeare",
                    DateOfBirth = new DateTime(1564, 04, 14)
                },
                new Author
                {
                    FirstName = "Stefan",
                    LastName = "Zweig",
                    DateOfBirth = new DateTime(1881, 11, 28)
                }
                );
            context.Genres.AddRange(
                new Genre
                {
                    Name = "Personal Growth"
                },
                new Genre
                {
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Name = "Romance"
                }
                );

            context.Books.AddRange(
                new Book
                {
                    Title = "Lean Startup",
                    GenreId = 1,
                    AuthorId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12)
                },
                new Book
                {
                    Title = "Herland",
                    GenreId = 2,
                    AuthorId = 1,
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23)
                },
                new Book
                {
                    Title = "Dune",
                    GenreId = 2,
                    AuthorId = 1,
                    PageCount = 540,
                    PublishDate = new DateTime(2001, 12, 21)
                }
            );

            context.SaveChanges();
        }
    }
}
