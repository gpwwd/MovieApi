using System.Net;
namespace MovieApiMvc.Models.Dtos;

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);

public record ExceptionResponseDevelopment(HttpStatusCode StatusCode, string Description, string? StackTrace) : ExceptionResponse(StatusCode, Description);