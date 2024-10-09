namespace Application.Dtos.UpdateDtos;

public record UserUpdateDto()
{
    public required string UserName { get; set; }
    public required string Password { get; set; } = String.Empty;
    public required string Email { get; set; } = String.Empty;
    public List<Guid>? FavMoviesIds { get; set; } = new List<Guid>();
    public List<Guid>? WatchLaterMoviesIds { get; set; } = new List<Guid>();
};