using Books.Service.Core.Entites;
using Books.Service.Core.Interfaces;
using Books.Service.Core.Requests;
using MediatR;

namespace Books.Service.Core.Handlers;

public class DeleteBookHandler : IRequestHandler<DeleteBookRequest, Book?>
{
    private readonly IBookRepository _bookRepository;

    public DeleteBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book?> Handle(DeleteBookRequest request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookAsync(request.Id);
        if(book != null)
        {
            await _bookRepository.DeleteAsync(book.Id);
        }

        return book;
    }
}