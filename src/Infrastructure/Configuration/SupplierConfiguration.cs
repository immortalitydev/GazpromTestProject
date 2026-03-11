using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("suppliers");

        builder.HasKey(s => s.Id);
        builder
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

        builder
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(s => s.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp without time zone");

        builder.HasData(
            new Supplier(1, "ООО Автопоставка", new DateTime(2023, 1, 15)),
            new Supplier(2, "ИП Моторс", new DateTime(2023, 2, 20)),
            new Supplier(3, "АвтоТрейд Групп", new DateTime(2023, 3, 10)),
            new Supplier(4, "Премиум Авто", new DateTime(2023, 4, 5)),
            new Supplier(5, "Глобал Карс", new DateTime(2023, 5, 12))
        );
    }
}
