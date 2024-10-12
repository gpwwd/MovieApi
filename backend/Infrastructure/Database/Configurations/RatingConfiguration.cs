using Domain.Entities.MovieEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<RatingEntity>
{
    public void Configure(EntityTypeBuilder<RatingEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(r => r.Id);

        entityTypeBuilder.HasMany(m => m.Movies)
            .WithOne(m => m.Rating)
            .OnDelete(DeleteBehavior.Restrict);

        entityTypeBuilder.ToTable("Ratings");
    }
}