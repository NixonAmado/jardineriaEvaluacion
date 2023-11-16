
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Data;

class StateConfiguration:IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        builder.ToTable("state");
        builder.HasIndex(e => e.CityId, "city_id_idx");
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.CityId).HasColumnName("city_id");
        builder.Property(e => e.Name)
            .HasMaxLength(45)
            .HasColumnName("name");
        builder.HasOne(d => d.City).WithMany(p => p.States)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("city_id");

       
    }
}  
  
 