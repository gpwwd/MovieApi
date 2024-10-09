using Domain.Entities.UsersEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasData(
            new RoleEntity()
            {
                Id = Guid.NewGuid(),
                Name = "User",
                NormalizedName = "USER"
            },
            new RoleEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new RoleEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Subscriber",
                NormalizedName = "SUBSCRIBER"
            }
        );
    }
}