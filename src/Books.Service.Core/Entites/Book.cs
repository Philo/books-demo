
namespace Books.Service.Core.Entites;

public record Book
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set;}
    public Book(string title, string author, decimal price)
    {
        Title = title;
        Author = author;
        Price = price;
    }
}
