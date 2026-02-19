namespace Api.Features.Catalog.Dtos;

// ==============================================================================
// FEATURE: GET PRODUCTS BY TYPE (Catálogo)
// Endpoint: GET /products/type/{productType}
// Uso: Tarjetas pequeñas en la página principal o resultados de búsqueda.
// ==============================================================================

public class FeaturedProductVariantDto
{
    public string Id { get; set; } = string.Empty; 
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string MainImageUrl { get; set; } = string.Empty;
    public bool HasStock { get; set; }
    public List<ColorVariantSummaryDto> Colors { get; set; } = new();
}

public class ColorVariantSummaryDto
{
    public string VariantId { get; set; } = string.Empty;
    public string ColorName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}