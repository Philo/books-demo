using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Books.Service.Web.Models;

public record BookDto
{
    [JsonInclude]
    public long Id { get; private set; }

    [Required]
    public string Title { get; init; }
    
    [Required]
    public string Author { get; init; }

    [Required]
    [Range(0, 9999)]
    public decimal Price { get; init; }

    public BookDto(string title, string author, decimal price)
    {
        Title = title;
        Author = author;
        Price = price;
    }
};