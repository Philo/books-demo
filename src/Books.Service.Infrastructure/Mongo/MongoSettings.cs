namespace Books.Service.Infrastructure.Mongo;

public record MongoSettings 
{
    public string? ConnectionString { get; set;}
    public string? DatabaseName { get; set; }
    public string? BooksCollectionName { get; set; }
}