using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// EF Core configuration for SaleItem entity.
/// </summary>
public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(si => si.Id);
        builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(si => si.ProductName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(si => si.ProductCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(si => si.Quantity)
            .IsRequired();

        builder.Property(si => si.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(si => si.DiscountPercentage)
            .IsRequired()
            .HasColumnType("decimal(5,4)");

        builder.Property(si => si.DiscountAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(si => si.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(si => si.IsCancelled)
            .IsRequired()
            .HasDefaultValue(false);

        // Foreign key to Sale
        builder.Property<Guid>("SaleId").IsRequired();

        // Add indexes for common queries
        builder.HasIndex(si => si.ProductCode);
        builder.HasIndex(si => si.IsCancelled);
        builder.HasIndex("SaleId");
    }
}