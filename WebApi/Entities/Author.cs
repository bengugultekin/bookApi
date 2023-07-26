using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi;

public class Author
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsPublished { get; set; } = false;

}
