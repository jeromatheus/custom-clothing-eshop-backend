using Api.Features.Purchase.Models;
using Api.Features.Purchase.Models.Enums;
namespace Api.Features.Catalog.Models;

public class Product
{
    public Guid Id { get; private set; }
    public string Sku { get; private set; } = default!;
    public ProductType Type { get; private set; }
    public decimal Price { get; private set; } = default!;
    // TODO: refactorizar a privado para evitar Clear()
    public List<ProductVariant> Variants { get; set; } = new();
    public List<ProductImageGroup> ImageGroups { get; set; } = new();
}

//private readonly List<ProductVariant> _variants = new();
//public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();