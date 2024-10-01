using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApiMvc.DataBaseAccess.Entities.MovieEntities;
using MovieApiMvc.DataBaseAccess.Entities.UsersEntities;

namespace MovieApiMvc.DataBaseAccess.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntitiy>
{
    public void Configure(EntityTypeBuilder<RoleEntitiy> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(u => u.Id);

        entityTypeBuilder.HasMany(u => u.Users)
            .WithOne(m => m.Role)
            .HasForeignKey(u => u.Role);
        
        entityTypeBuilder.HasData(
            new RoleEntitiy
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
            },
            new RoleEntitiy
            {
                Id = Guid.NewGuid(),
                Name = "User",
            });
    }
}