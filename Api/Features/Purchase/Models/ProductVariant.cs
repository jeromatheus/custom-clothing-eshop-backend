using Api.Features.Catalog.Models;
using Api.Features.Purchase.Models.Enums;
namespace Api.Features.Purchase.Models;

public class ProductVariant
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int Stock { get; private set; }
    public Product? Product { get; private set; }
    public Color Color { get; private set; }
    public Size Size { get; private set; }
    public Fabric Fabric { get; private set; }
    public NeckType? NeckType { get; private set; }
    public Fit? Fit { get; private set; }
    public WarmthLevel WarmthLevel { get; private set; }
    public List<ProductMeasurement> Measurements { get; set; } = new();
}
