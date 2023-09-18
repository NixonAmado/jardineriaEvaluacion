
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class RolConfiguration:IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("Rol");
        builder.Property("Name")
        .IsRequired()
        .HasMaxLength(40);

        builder
        .HasMany(p => p.Users)
        .WithMany(p => p.Rols)
        .UsingEntity<UserRol>(

            j => j
            .HasOne(p => p.User)
            .WithMany(p => p.UsersRols)
            .HasForeignKey(p => p.IdUserFk),
            j => j
            .HasOne(p => p.Rol)
            .WithMany(p => p.UsersRols)
            .HasForeignKey(p => p.IdRolFk),
            j =>
            {
                j.HasKey(t => new {t.IdRolFk,t.IdUserFk});
            });
    }
}