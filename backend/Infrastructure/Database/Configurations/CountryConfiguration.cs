using Domain.Entities.MovieEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<CountryEntity>
{
    public void Configure(EntityTypeBuilder<CountryEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(c => c.Id);

        entityTypeBuilder.ToTable("Countries");
    }
}