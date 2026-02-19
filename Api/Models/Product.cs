using Api.Models.Enums;
namespace Api.Models;

public class Product
{
    public Guid Id { get; private set; }
    public Garment Garment { get; private set; }
    public Neck Neck { get; private set; }
    public Fit Fit { get; private set; }
    public Material Material { get; private set; }
    public Warmth Warmth { get; private set; }
    public double Price { get; private set; }
    public List<Variant> Variants { get; private set; } = new();
    public List<SizeMeasurement> SizeMeasurements { get; private set; } = new();

    private Product() { }

    public Product(Garment garment, Neck neck, Fit fit, Material material, Warmth warmth, double price)
    {
        Id = Guid.NewGuid();
        Garment = garment;
        Neck = neck;
        Fit = fit;
        Material = material;
        Warmth = warmth;
        Price = price;
    }

    public string GetFullName()
    {
        return $"{Garment} {Neck} {Fit} {Material} {Warmth}";
    }

    public void AddVariant(Variant variant)
    {
        Variants.Add(variant);
    }

    public void AddSizeMeasurement(SizeMeasurement measurement)
    {
        SizeMeasurements.Add(measurement);
    }

}
