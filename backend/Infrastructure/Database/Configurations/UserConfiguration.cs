using Domain.Entities.UsersEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(u => u.Id);
        entityTypeBuilder.ToTable("Users");

        entityTypeBuilder.HasMany(u => u.FavMovies)
            .WithMany(m => m.FavMovieUsers)
            .UsingEntity(j => j.ToTable("FavMovieUsers"));
            
        entityTypeBuilder.HasMany(u => u.WatchLaterMovies)
            .WithMany(m => m.WatchLaterUsers)
            .UsingEntity(j => j.ToTable("WatchLaterMoviesUsers"));
    }
}

