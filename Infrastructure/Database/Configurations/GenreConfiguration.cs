using Domain.Entities.MovieEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<GenreEntity>
{
    public void Configure(EntityTypeBuilder<GenreEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(g => g.Id);

        entityTypeBuilder.ToTable("Genres");
    }
}