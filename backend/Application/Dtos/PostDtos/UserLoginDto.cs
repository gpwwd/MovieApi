using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.PostDtos
{
    public class UserLoginDto
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; } = String.Empty;
    }
}