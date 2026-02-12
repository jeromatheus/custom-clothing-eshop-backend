using Api.Features.Purchase.Models.Enums;

namespace Api.Features.Catalog.Models;

public class ProductImageGroup
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    // Datos del modelo con imágenes perteneciéntes a este modelo específico
    public int? ModelHeight { get; private set; }
    public Size? ModelWearingSize { get; private set; }
    public List<ProductImage> Images { get; set; } = new();

    private ProductImageGroup() { }

    public ProductImageGroup(Guid productId, string name, int? modelHeight, Size? modelWearingSize, List<ProductImage> images)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ModelHeight = modelHeight;
        ModelWearingSize = modelWearingSize;
        Images = images;
    }
}