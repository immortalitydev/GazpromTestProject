using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("offers");

        builder.HasKey(o => o.Id);
        builder
            .Property(o => o.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(o => o.Brand)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(o => o.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(o => o.RegistrationDate)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder
            .Property(o => o.SupplierId)
            .IsRequired();

        builder
            .HasIndex(o => o.Brand)
            .HasDatabaseName("IX_Offers_Brand");

        builder
            .HasIndex(o => o.Model)
            .HasDatabaseName("IX_Offers_Model");

        builder
            .HasOne(o => o.Supplier)
            .WithMany(s => s.Offers)
            .HasForeignKey(o => o.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Offer(1, "Toyota", "Camry", 1, new DateTime(2024, 1, 10, 10, 30, 0)),
            new Offer(2, "Honda", "Accord", 1, new DateTime(2024, 1, 15, 14, 20, 0)),
            new Offer(3, "BMW", "X5", 2, new DateTime(2024, 1, 20, 9, 15, 0)),
            new Offer(4, "Mercedes", "E-Class", 1, new DateTime(2024, 2, 5, 16, 45, 0)),
            new Offer(5, "Audi", "A6", 3, new DateTime(2024, 2, 10, 11, 0, 0)),
            new Offer(6, "Toyota", "RAV4", 2, new DateTime(2024, 2, 15, 13, 30, 0)),
            new Offer(7, "Volkswagen", "Tiguan", 1, new DateTime(2024, 2, 20, 10, 10, 0)),
            new Offer(8, "Ford", "Focus", 4, new DateTime(2024, 3, 1, 15, 25, 0)),
            new Offer(9, "Hyundai", "Tucson", 1, new DateTime(2024, 3, 5, 12, 40, 0)),
            new Offer(10, "Kia", "Sportage", 3, new DateTime(2024, 3, 10, 9, 50, 0)),
            new Offer(11, "Mazda", "CX-5", 2, new DateTime(2024, 3, 15, 14, 5, 0)),
            new Offer(12, "Nissan", "Qashqai", 5, new DateTime(2024, 3, 20, 11, 20, 0)),
            new Offer(13, "Skoda", "Octavia", 3, new DateTime(2024, 3, 25, 16, 35, 0)),
            new Offer(14, "Volvo", "XC60", 2, new DateTime(2024, 4, 1, 10, 0, 0)),
            new Offer(15, "Lexus", "RX", 1, new DateTime(2024, 4, 5, 13, 15, 0))
        );
    }
}
