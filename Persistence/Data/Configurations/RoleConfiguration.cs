
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class RoleConfiguration:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        builder.Property(p => p.Description)
        .IsRequired()
        .HasMaxLength(40);

        builder
        .HasMany(p => p.Users)
        .WithMany(p => p.Roles)
        .UsingEntity<UserRole>(

            j => j
            .HasOne(p => p.User)
            .WithMany(p => p.UsersRoles)
            .HasForeignKey(p => p.IdUserFk),
            j => j
            .HasOne(p => p.Role)
            .WithMany(p => p.UsersRoles)
            .HasForeignKey(p => p.IdRolFk),
            j =>
            {
                j.HasKey(t => new {t.IdRolFk,t.IdUserFk});
            });
    }
}