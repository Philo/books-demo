namespace Books.Service.Infrastructure.Settings;

public record DatabaseSettings 
{
    public string ConnectionString { get; set;}
    public string DatabaseName { get; set; }
    public string BooksCollectionName { get; set; }

    public DatabaseSettings(string connectionString, string databaseName, string booksCollectionName)
    {
        ConnectionString = connectionString;
        DatabaseName = databaseName;
        BooksCollectionName = booksCollectionName;
    }
}