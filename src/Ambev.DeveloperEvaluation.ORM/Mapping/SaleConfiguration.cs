using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// EF Core configuration for Sale entity.
/// </summary>
public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.SaleNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(s => s.SaleNumber)
            .IsUnique();

        builder.Property(s => s.SaleDate)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(s => s.CustomerName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.CustomerEmail)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.CustomerDocument)
            .HasMaxLength(20);

        builder.Property(s => s.BranchName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.BranchCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.IsCancelled)
            .IsRequired()
            .HasDefaultValue(false);

        // Configure relationship with SaleItems
        builder.HasMany(s => s.Items)
            .WithOne()
            .HasForeignKey("SaleId")
            .OnDelete(DeleteBehavior.Cascade);

        // Configure timestamps
        builder.Property(s => s.SaleDate)
            .IsRequired()
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Add indexes for common queries
        builder.HasIndex(s => s.CustomerName);
        builder.HasIndex(s => s.BranchCode);
        builder.HasIndex(s => s.SaleDate);
        builder.HasIndex(s => s.IsCancelled);
    }
}