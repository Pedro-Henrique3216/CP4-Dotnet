using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Mapping
{
    internal class SectorMapping : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.ToTable("sectors");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(s => s.YardId)
                   .HasColumnName("yard_id")
                   .IsRequired();

            builder.Property(s => s.SectorTypeId)
                   .HasColumnName("sector_type_id")
                   .IsRequired();

            builder.HasOne(s => s.Yard)
                   .WithMany(y => y.Sectors)
                   .HasForeignKey(s => s.YardId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.SectorType)
                   .WithMany()
                   .HasForeignKey(s => s.SectorTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Points)
                   .WithOne(p => p.Sector)
                   .HasForeignKey(p => p.SectorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Spots)
                   .WithOne(sp => sp.Sector)
                   .HasForeignKey(sp => sp.SectorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
