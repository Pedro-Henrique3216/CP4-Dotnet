using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Mapping
{
    internal class PolygonPointMapping : IEntityTypeConfiguration<PolygonPoint>
    {
        public void Configure(EntityTypeBuilder<PolygonPoint> builder)
        {
            builder.ToTable("polygon_points");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(p => p.PointOrder)
                   .HasColumnName("point_order")
                   .IsRequired();

            builder.Property(p => p.X)
                   .HasColumnName("x")
                   .IsRequired();

            builder.Property(p => p.Y)
                   .HasColumnName("y")
                   .IsRequired();

            builder.Property(p => p.YardId)
                   .HasColumnName("yard_id");

            builder.Property(p => p.SectorId)
                   .HasColumnName("sector_id");

            builder.HasOne(p => p.Yard)
                   .WithMany(y => y.Points)
                   .HasForeignKey(p => p.YardId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Sector)
                   .WithMany(s => s.Points)
                   .HasForeignKey(p => p.SectorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
