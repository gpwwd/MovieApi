using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApiMvc.DataBaseAccess.Entities;

namespace MovieApiMvc.DataBaseAccess.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<GenreEntity>
{
    public void Configure(EntityTypeBuilder<GenreEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(g => g.Id);

        entityTypeBuilder.ToTable("Genres");
    }
}