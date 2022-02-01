using Books.Service.Core.Entites;
using MediatR;

namespace Books.Service.Core.Requests;

public class GetBookRequest : IRequest<Book?>
{
    public long Id { get; init; }

    public GetBookRequest(long id)
    {
        Id = id;
    }
}

