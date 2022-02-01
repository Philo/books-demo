
using Books.Service.Core.Entites;
using MediatR;

namespace Books.Service.Core.Requests;

public record CreateBookRequest : IRequest<Book>
{
    public Book NewBook { get; init; }

    public CreateBookRequest(Book newBook)
    {
        NewBook = newBook;
    }
}