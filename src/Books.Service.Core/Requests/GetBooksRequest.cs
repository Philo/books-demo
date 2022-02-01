

using System.Collections.Generic;
using Books.Service.Core.Entites;
using Books.Service.Core.Enums;
using MediatR;

namespace Books.Service.Core.Requests;

public class GetBooksRequest : IRequest<IEnumerable<Book>>
{
    public SortBy SortBy { get; init; }

    public GetBooksRequest(SortBy sortBy)
    {
        SortBy = sortBy;
    }
}

