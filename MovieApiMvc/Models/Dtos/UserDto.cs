namespace MovieApiMvc.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }  
        public required string UserName { get; set; }
        public required string Password { get; set; } = String.Empty;
        public required string Email { get; set; } = String.Empty;
        public List<MovieDto>? FavMovies {get; set;} = default;
        public List<MovieDto>? WatchLaterMovies {get; set;} = default;
    }
}