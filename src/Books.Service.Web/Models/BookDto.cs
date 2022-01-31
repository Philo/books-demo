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
    public decimal Price { get; init; }

    public BookDto(string title, string author, decimal price)
    {
        Title = title;
        Author = author;
        Price = price;
    }
};