namespace Api.Model;

public class StockItem
{
    public Guid Id { get; private set; }
    public Guid VariableAttributeId { get; private set; }
    public VariableAttribute VariableAttribute { get; private set; } = default!;
    public Size Size { get; private set; }
    public int Quantity { get; private set; }

    private StockItem() { }

    public StockItem(Guid variableAttributeId, Size size, int quantity)
    {
        Id = Guid.NewGuid();
        VariableAttributeId = variableAttributeId;
        Size = size;
        Quantity = quantity;
    }
}
