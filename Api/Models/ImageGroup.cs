using Api.Models.Enums;
namespace Api.Models;

public class ImageGroup
{
    public Guid Id { get; private set; }
    public Guid VariantId { get; private set; }
    public Variant Variant { get; private set; } = default!;
    public int? ModelHeight { get; private set; }
    public Size? ModelWearingSize { get; private set; }
    public List<Image> Images { get; private set; } = new();

    private ImageGroup() { } 

    public ImageGroup(Guid variantId, int? modelHeight, Size? modelWearingSize)
    {
        Id = Guid.NewGuid();
        VariantId = variantId;
        ModelHeight = modelHeight;
        ModelWearingSize = modelWearingSize;
    }
}