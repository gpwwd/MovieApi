using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.UpdateDtos;

public record UpdateNameDto
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(70, ErrorMessage = "Maximum length for the Name is 70 characters.")]
    public string Name { get; init; } = null!;
};