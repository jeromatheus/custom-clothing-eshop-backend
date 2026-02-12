using Api.Features.Purchase.Models.Enums;

namespace Api.Features.Purchase.Models;

public class ProductMeasurement
{
    public Guid Id { get; private set; }
    public Guid ProductVariantId { get; private set; }
    public Size Size { get; private set; }
    public string MeasurementName { get; private set; } = string.Empty;
    public decimal Value { get; private set; }
    public string Unit { get; private set; } = "cm";

    private ProductMeasurement() { }

    public ProductMeasurement(Guid productVariantId, Size size, string name, decimal value, string unit = "cm")
    {
        Id = Guid.NewGuid();
        ProductVariantId = productVariantId;
        Size = size;
        MeasurementName = name;
        Value = value;
        Unit = unit;
    }
}