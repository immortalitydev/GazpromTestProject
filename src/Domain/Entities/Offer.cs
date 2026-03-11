namespace Domain.Entities;

public class Offer : Entity
{
    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int SupplierId { get; set; }

    public Supplier Supplier { get; set; } = null!;

    public DateTime RegistrationDate { get; set; } = DateTime.Now;

    public Offer()
    {
    }

    public Offer(int id, string brand, string model, int supplierId, DateTime registrationDate)
    {
        Id = id;
        Brand = brand;
        Model = model;
        SupplierId = supplierId;
        RegistrationDate = registrationDate;
    }
}
