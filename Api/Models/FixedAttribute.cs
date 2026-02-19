using Api.Model;

public class FixedAttribute
{
    public Guid Id { get; private set; }

    public Garment Garment { get; private set; }
    public Neck Neck { get; private set; }
    public Fit Fit { get; private set; }
    public Material Material { get; private set; }
    public WarmthLevel WarmthLevel { get; private set; }
    public double Price { get; private set; }

    public List<VariableAttribute> VariableAttributes { get; private set; } = new();

    private FixedAttribute() { }

    public FixedAttribute(Garment garment, Neck neck, Fit fit, Material material, WarmthLevel warmthLevel, double price)
    {
        Id = Guid.NewGuid();
        Garment = garment;
        Neck = neck;
        Fit = fit;
        Material = material;
        WarmthLevel = warmthLevel;
        Price = price;
    }

    public string GetFullName()
    {
        return $"{Garment} {Neck} {Fit} {Material} {WarmthLevel}";
    }

}


public enum Garment
{
    LongSleeveTShirt,
    ShortSleeveTShirt,
    PoloShirt
}

public enum Material
{
    Cotton,
    Polyester,
    Silk
}

public enum Neck
{
    CrewNeck,
    VNeck,
    Polo
}

public enum Fit
{
    Slim,
    Regular,
    Oversize
}

public enum WarmthLevel
{
    Low,
    Medium,
    High
}