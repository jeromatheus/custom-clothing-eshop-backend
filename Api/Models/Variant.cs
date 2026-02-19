using Api.Models.Enums;
namespace Api.Models;

public class Variant
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;
    public Color Color { get; private set; }
    public List<StockItem> StockItems { get; private set; } = new();
    public List<ImageGroup> ImageGroups { get; private set; } = new();

    private Variant() { } 

    public Variant(Guid productId, Color color)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Color = color;
    }
}
