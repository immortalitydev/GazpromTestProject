namespace Application.Offers.Dtos;

public class CreateOfferRequest
{
    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int SupplierId { get; set; }
}