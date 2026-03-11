namespace Domain.Entities;

public class Supplier : Entity
{
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Offer> Offers { get; set; } = new List<Offer>();

    public Supplier()
    {
    }

    public Supplier(int id, string name, DateTime createdAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
    }
}
