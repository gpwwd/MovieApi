namespace MovieApiMvc.Models.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }  
        public required string UserName { get; set; }
        public required string Password { get; set; } = String.Empty;
        public required string Email { get; set; } = String.Empty;
        public List<MovieDto>? FavMovies { get; set; } = new List<MovieDto>();
        public List<MovieDto>? WatchLaterMovies { get; set; } = new List<MovieDto>();
    }
}