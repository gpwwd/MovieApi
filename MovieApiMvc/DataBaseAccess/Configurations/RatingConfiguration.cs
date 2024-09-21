using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApiMvc.DataBaseAccess.Entities;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;

namespace MovieApiMvc.DataBaseAccess.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<RatingEntity>
{
    public void Configure(EntityTypeBuilder<RatingEntity> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(r => r.Id);

        entityTypeBuilder.ToTable("Ratings");
    }
    //указать впоследствии какие поля должны быть обязательными(те что на клиенте используются)
}