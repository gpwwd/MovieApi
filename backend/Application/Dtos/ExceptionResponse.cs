using System.Net;

namespace Application.Dtos;

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);

public record ExceptionResponseDevelopment(HttpStatusCode StatusCode, string Description, string? StackTrace) : ExceptionResponse(StatusCode, Description);