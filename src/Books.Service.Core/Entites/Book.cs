
namespace Books.Service.Core.Entites;

public record Book
{
    public long Id { get; init; }
    public string Title { get; init; }
    public string Author { get; init; }
    public decimal Price { get; init;}
    public Book() { }
    public Book(string title, string author, decimal price)
    {
        Title = title;
        Author = author;
        Price = price;
    }
}
