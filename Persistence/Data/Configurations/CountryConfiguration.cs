
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class CountryConfiguration:IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("country");

        builder.HasIndex(e => e.StateId, "state_id_idx");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Name)
            .HasMaxLength(45)
            .HasColumnName("name");
        builder.Property(e => e.StateId).HasColumnName("state_id");

        builder.HasOne(d => d.State).WithMany(p => p.Countries)
            .HasForeignKey(d => d.StateId)
            .HasConstraintName("state_id");

       
    }
}  
  
 