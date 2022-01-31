using System.ComponentModel.DataAnnotations;
using Books.Service.Web.Models;
using Books.Service.Web.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Books.Service.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{   
    [HttpPost()]
    [Consumes(typeof(BookDto), "application/json")]
    [SwaggerOperation(OperationId = "create-book", Summary = "Creates a new book")]
    [SwaggerResponse(StatusCodes.Status201Created, "Created", typeof(CreateSuccessResponse), "application/json")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(BadRequestResponse), "application/json")]
    public ActionResult<BookDto> CreateBook([Required] BookDto book) 
    {
        throw new NotImplementedException();
    }      

    [HttpGet()] 
    [SwaggerOperation(OperationId = "get-book", Summary = "Returns a list of books. Sorted by title by default.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(BookDto), "application/json")]
    public IEnumerable<BookDto> GetBook(string sortby)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    [Consumes(typeof(BookDto), "application/json")]
    [SwaggerOperation(OperationId = "update-book-by-id", Summary = "Update an existing book")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(BadRequestResponse), "application/json")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
    public ActionResult UpdateBookById([Required] long id, [Required] [FromBody] BookDto book)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(OperationId = "get-book-by-id", Summary = "Gets a book by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(BookDto), "application/json")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
    public ActionResult GetBookById([Required] long id)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(OperationId = "delete-book-by-id", Summary = "Deletes a book by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
    public ActionResult DeleteBookById([Required] long id)
    {
        throw new NotImplementedException();
    }
}
