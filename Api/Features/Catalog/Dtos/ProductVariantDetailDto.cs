namespace Api.Features.Catalog.Dtos;

public class ProductVariantDetailDto
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string GarmentType { get; set; } = string.Empty;
    public string NeckType { get; set; } = string.Empty;
    public string FitType { get; set; } = string.Empty;
    public string MaterialType { get; set; } = string.Empty;
    public int WarmthLevel { get; set; }
    public List<SizeSpecDto> SizeChart { get; set; } = new();
    public List<ColorVariantDto> Variants { get; set; } = new(); 
    public List<ModelDto> Models { get; set; } = new(); 
}

public class SizeSpecDto
{
    public string Size { get; set; } = string.Empty;
    public double Chest { get; set; }
    public double Length { get; set; }
    public double Neck { get; set; }
}

public class ColorVariantDto
{
    public Guid VariantId { get; set; }
    public string ColorName { get; set; } = string.Empty;
    public List<SizeStockDto> Sizes { get; set; } = new();
}

public class SizeStockDto
{
    public string Size { get; set; } = string.Empty;
    public int Stock { get; set; }
}

public class ModelDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty; 
    public string ColorName { get; set; } = string.Empty; 
    public string HeightInfo { get; set; } = string.Empty;
    public string SizeInfo { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public List<string> CarouselImages { get; set; } = new();
}