namespace WebApi.UnitTests;

public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
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
    }
}
