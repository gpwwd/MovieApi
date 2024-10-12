using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Application.Dtos.UpdateDtos;

public record UserUpdateDto()
{
    [Required(ErrorMessage = "Username is required")]
    [UsernameUnique]
    public required string UserName { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public required string Password { get; set; } = String.Empty;
    
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [EmailUserUnique]
    public required string Email { get; set; } = String.Empty;
    public List<Guid>? FavMoviesIds { get; set; } = new List<Guid>();
    public List<Guid>? WatchLaterMoviesIds { get; set; } = new List<Guid>();
};