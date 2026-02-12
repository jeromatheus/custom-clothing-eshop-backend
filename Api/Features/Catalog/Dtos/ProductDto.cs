namespace Api.Features.Catalog.Dtos;

// ==============================================================================
// 1. FEATURE: GET PRODUCTS BY TYPE (Catálogo / Grilla)
// Endpoint: GET /products/type/{productType}
// Uso: Tarjetas pequeñas en la página principal o resultados de búsqueda.
// ==============================================================================

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string MainImageUrl { get; set; } = string.Empty;
    public List<string> AvailableColors { get; set; } = new();
    public bool HasStock { get; set; }
}