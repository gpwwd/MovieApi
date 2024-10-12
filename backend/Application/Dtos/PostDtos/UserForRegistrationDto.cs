using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Application.Dtos.PostDtos;
public record UserForRegistrationDto
{
    [Required(ErrorMessage = "Username is required")]
    [UsernameUnique]
    public string UserName { get; init; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; init; } = null!;

    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [EmailUserUnique]
    public string Email { get; init; } = String.Empty;
    
    [Roles]
    public ICollection<string>? Roles { get; init; }
}