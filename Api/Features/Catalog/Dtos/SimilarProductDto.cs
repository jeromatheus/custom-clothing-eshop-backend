namespace Api.Features.Catalog.Dtos;

// ==============================================================================
// FEATURE: GET PRODUCTS BY TYPE (Catálogo)
// Endpoint: GET /products/type/{productType}
// Uso: Tarjetas pequeñas en la página principal o resultados de búsqueda.
// ==============================================================================

public class SimilarProductDto
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string MainImageUrl { get; set; } = string.Empty;
    public List<string> AvailableColors { get; set; } = new();
    public bool HasStock { get; set; }
}