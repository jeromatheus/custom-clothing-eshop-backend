namespace Api.Features.Catalog.Models;

public class ProductImage
{
    public Guid Id { get; private set; }
    public Guid ProductImageGroupId { get; private set; }
    public string ImageUrl { get; private set; } = default!;
    public bool IsMain { get; private set; } 

    private ProductImage() { }

    public ProductImage(Guid productImageGroupId, string imageUrl, bool isMain)
    {
        Id = Guid.NewGuid();
        ProductImageGroupId = productImageGroupId;
        ImageUrl = imageUrl;
        IsMain = isMain;
    }
}