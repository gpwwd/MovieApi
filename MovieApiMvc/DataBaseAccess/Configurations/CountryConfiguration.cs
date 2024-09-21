using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;

namespace MovieApiMvc.DataBaseAccess.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<CountryEntity>
{
    public void Configure(EntityTypeBuilder<CountryEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(c => c.Id);

        entityTypeBuilder.ToTable("Countries");
    }
}