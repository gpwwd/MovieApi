using System.ComponentModel.DataAnnotations;

namespace MovieApiMvc.Models.Dtos.PostDtos;
public record UserForRegistrationDto
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; init; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; init; } = null!;

    public string? Email { get; init; }
    public ICollection<string>? Roles { get; init; }
}