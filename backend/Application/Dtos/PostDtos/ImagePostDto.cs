namespace Application.Dtos.PostDtos;

public record ImagePostDto
{
    public List<string>? Urls { get; set; }
    public List<string>? PreviewUrls { get; set; }
};