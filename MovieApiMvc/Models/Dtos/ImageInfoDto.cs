namespace MovieApiMvc.Models.Dtos
{
    public class ImageInfoDto
    {
        public Guid Id { get; set; }
        public List<string>? Urls { get; set; }
        public List<string>? PreviewUrls { get; set; }
        public Guid MovieId { get; set; }
    }
}