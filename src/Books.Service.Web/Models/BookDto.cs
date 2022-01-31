using System.ComponentModel.DataAnnotations;

namespace Books.Service.Web.Models;

public record BookDto
{
    public long Id { get; private set; }

    [Required]
    public string Title { get; init; }
    
    [Required]
    public string Author { get; init; }

    [Required]
    public double Price { get; init; }

    public BookDto(string title, string author, double price)
    {
        Title = title;
        Author = author;
        Price = price;
    }
};