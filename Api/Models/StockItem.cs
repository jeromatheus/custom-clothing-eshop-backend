using Api.Models.Enums;
namespace Api.Models;

public class StockItem
{
    public Guid Id { get; private set; }
    public string Sku { get; private set; } = string.Empty;
    public Guid VariantId { get; private set; }
    public Variant Variant { get; private set; } = default!;
    public Size Size { get; private set; }
    public int Quantity { get; private set; }

    private StockItem() { }

    public StockItem(Guid variantId, Size size, int quantity)
    {
        Id = Guid.NewGuid();
        VariantId = variantId;
        Size = size;
        Quantity = quantity;
    }
}
