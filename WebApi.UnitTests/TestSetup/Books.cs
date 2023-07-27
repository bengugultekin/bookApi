﻿namespace WebApi.UnitTests;

public static class Books
{
    public static void AddBooks(this BookStoreDbContext context)
    {
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
    }
}