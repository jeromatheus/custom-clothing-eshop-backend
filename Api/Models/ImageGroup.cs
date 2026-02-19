namespace Api.Model;

public class ImageGroup
{
    public Guid Id { get; private set; }

    public Guid VariableAttributeId { get; private set; }
    public VariableAttribute VariableAttribute { get; private set; } = default!;

    public int? ModelHeight { get; private set; }
    public Size? ModelWearingSize { get; private set; }

    public List<Image> Images { get; private set; } = new();

    private ImageGroup() { } // EF

    public ImageGroup(Guid variableAttributeId, int? modelHeight, Size? modelWearingSize)
    {
        Id = Guid.NewGuid();
        VariableAttributeId = variableAttributeId;
        ModelHeight = modelHeight;
        ModelWearingSize = modelWearingSize;
    }
}