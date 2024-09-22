using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities.UsersEntities;

namespace MovieApiMvc.DataBaseAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(u => u.Id);
        entityTypeBuilder.ToTable("Users");

        entityTypeBuilder.HasMany(u => u.FavMovies)
            .WithMany(m => m.FavMovieUsers);
            
        entityTypeBuilder.HasMany(u => u.WatchLaterMovies)
            .WithMany(m => m.WatchLaterUsers);

    }
}

