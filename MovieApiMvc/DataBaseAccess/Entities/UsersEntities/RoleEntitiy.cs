namespace MovieApiMvc.DataBaseAccess.Entities.UsersEntities;

public class RoleEntitiy
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<UserEntity>? Users { get; set; }
}