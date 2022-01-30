using System.ComponentModel;

namespace Books.Service.Web.Responses;
public record CreateSuccessResponse([Description("Id of the created book")] long Id);
public record BadRequestResponse([Description("Error messages")] IEnumerable<object> Errors);