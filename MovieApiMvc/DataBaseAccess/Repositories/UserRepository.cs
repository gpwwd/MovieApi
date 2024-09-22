using MovieApiMvc.DataBaseAccess.Entities.MovieEntities.UsersEntities;
using MovieApiMvc.DataBaseAccess.Repositories.Contracts;

namespace MovieApiMvc.DataBaseAccess.Repositories;

public class UserRepository : RepositoryBase<UserEntity>, IUserRepository 
{
    public UserRepository(MovieDataBaseContext context) 
        : base(context)
    {
        
    }
}