using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Application.Dtos.PostDtos;

public record ImagePostDto
{
    [UrlList]
    [Required]
    public List<string>? Urls { get; set; }
    
    [UrlList]
    public List<string>? PreviewUrls { get; set; }
}