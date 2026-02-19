using Api.Models.Enums;
namespace Api.Models;

public class SizeMeasurement
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;
    public Size Size { get; private set; }
    public double Chest { get; private set; }
    public double Length { get; private set; }
    public double Neck { get; private set; }

    private SizeMeasurement() { } 

    public SizeMeasurement(Guid productId, Size size, double chest, double length, double neck)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Size = size;
        Chest = chest;
        Length = length;
        Neck = neck;
    }
}