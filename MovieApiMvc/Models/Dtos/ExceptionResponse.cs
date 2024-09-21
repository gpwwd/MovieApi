using System.Net;
namespace MovieApiMvc.Models.Dtos;

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);