using System.ComponentModel.DataAnnotations;
namespace LibraryManagement.Database.Entities;

public class Book
{
    [Key]
    public int BookId { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    //public DateTime DateTime { get; set; }
}