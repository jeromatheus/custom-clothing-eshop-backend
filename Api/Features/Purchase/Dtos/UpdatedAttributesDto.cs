namespace Api.Features.Purchase.Dtos;

// ==============================================================================
// FEATURE: Obtener Detalles Actualizados por Filtros (GetUpdatedAttributes)
// Endpoint: GET /products/attributes?garment=...&fit=...&material=...
// Uso: Devuelve la información dinámica (Precio, Variantes, Stock, Fotos) 
//      basada en la combinación exacta de atributos fijos elegidos en el formulario.
// ==============================================================================
public class UpdatedAttributesDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string WarmthLevel { get; set; } = string.Empty;
    public List<SizeSpecDto> SizeChart { get; set; } = new();
    public List<ColorVariantDto> Variants { get; set; } = new();
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
    public List<string> Images { get; set; } = new();
    public List<SizeStockDto> Sizes { get; set; } = new();
}

public class SizeStockDto
{
    public string Size { get; set; } = string.Empty;
    public int Stock { get; set; }
    public bool Available => Stock > 0;
}