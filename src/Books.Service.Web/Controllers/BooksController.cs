using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Books.Service.Core.Entites;
using Books.Service.Core.Enums;
using Books.Service.Core.Requests;
using Books.Service.Web.Models;
using Books.Service.Web.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Books.Service.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{   
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public BooksController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost()]
    [Consumes(typeof(BookDto), "application/json")]
    [SwaggerOperation(OperationId = "create-book", Summary = "Creates a new book")]
    [SwaggerResponse(StatusCodes.Status201Created, "Created", typeof(CreateSuccessResponse), "application/json")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(BadRequestResponse), "application/json")]
    public async Task<ActionResult<CreateSuccessResponse>> CreateBook([Required] BookDto book) 
    {
        var response = await _mediator.Send(new CreateBookRequest(_mapper.Map<Book>(book)));
        return new CreateSuccessResponse(response.Id);
    }      

    [HttpGet()] 
    [SwaggerOperation(OperationId = "get-book", Summary = "Returns a list of books. Sorted by title by default.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(BookDto), "application/json")]
    public async Task<IEnumerable<BookDto>> GetBook(SortBy sortby = SortBy.Title)
    {
        var response = await _mediator.Send(new GetBooksRequest(sortby)); 
        return _mapper.Map<IEnumerable<BookDto>>(response);
    }

    [HttpPut("{id}")]
    [Consumes(typeof(BookDto), "application/json")]
    [SwaggerOperation(OperationId = "update-book-by-id", Summary = "Update an existing book")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(BadRequestResponse), "application/json")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
    public async Task<ActionResult> UpdateBookById([Required] long id, [Required] [FromBody] BookDto book)
    {
        var bookEntity = _mapper.Map<Book>(book);
        bookEntity.Id = id;

        var response = await _mediator.Send(new UpdateBookRequest(_mapper.Map(book, bookEntity)));

        return response != null ? new OkResult() : new NotFoundResult();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(OperationId = "get-book-by-id", Summary = "Gets a book by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(BookDto), "application/json")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
    public async Task<ActionResult<BookDto>> GetBookById([Required] long id)
    {
        var response = await _mediator.Send(new GetBookRequest(id));
        return response != null ? new OkObjectResult(_mapper.Map<BookDto>(response)) : new NotFoundResult();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(OperationId = "delete-book-by-id", Summary = "Deletes a book by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
    public async Task<ActionResult> DeleteBookById([Required] long id)
    {
        var response = await _mediator.Send(new DeleteBookRequest(id));
        return response != null ? new OkResult() : new NotFoundResult();
    }
}
