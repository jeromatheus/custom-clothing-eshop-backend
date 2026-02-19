namespace Api.Model;

public class VariableAttribute
{
    public Guid Id { get; private set; }

    public Guid FixedAttributeId { get; private set; }
    public FixedAttribute FixedAttribute { get; private set; } = default!;

    public Color Color { get; private set; }

    public List<StockItem> StockItems { get; private set; } = new();
    public List<ImageGroup> ImageGroups { get; private set; } = new();

    private VariableAttribute() { } 

    public VariableAttribute(Guid fixedAttributeId, Color color)
    {
        Id = Guid.NewGuid();
        FixedAttributeId = fixedAttributeId;
        Color = color;
    }
}

public enum Color
{
    Black,
    White,
    Gray,
    Red
}

public enum Size
{
    XS,
    S,
    M,
    L,
    XL,
    XXL
}
