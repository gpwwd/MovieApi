namespace MovieApiMvc.DataBaseAccess.Entities.MovieEntities
{
    public class ImageInfoEntity
    {
        public Guid Id { get; set; }
        public List<string> Urls { get; set; }
        public List<string> PreviewUrls { get; set; }
        public Guid MovieId { get; set; }
        public MovieEntity? Movie { get; set; }
    }
}