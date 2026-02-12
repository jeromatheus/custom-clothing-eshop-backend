namespace Api.Features.Purchase.Dtos;



// ==============================================================================
// 2. FEATURE: GET PRODUCT BY ID (Detalle del Producto)
// Endpoint: GET /products/{id}
// Uso: Página de compra (PurchasePage), selector de talles, carrusel completo.
// ==============================================================================

public class ProductDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Type { get; set; } = string.Empty;

    // Relaciones Jerárquicas
    public List<ProductImageGroupDto> ImageGroups { get; set; } = new();
    public List<ProductVariantDto> Variants { get; set; } = new();
}

// Sub-DTO: Grupos de Imágenes (Ej: "Modelo Hombre", "Flat Lay")
public class ProductImageGroupDto
{
    public int? ModelHeight { get; set; }      // Para mostrar "El modelo mide 1.82m"
    public string? ModelWearingSize { get; set; } // "El modelo usa talle L"
    public List<string> Images { get; set; } = new(); // Lista de URLs (Strings)
}

// Sub-DTO: Variantes Físicas (Stock y Selección)
public class ProductVariantDto
{
    public Guid Id { get; set; }
    public string Color { get; set; } = string.Empty; // "Red"
    public string Size { get; set; } = string.Empty;  // "M"
    public int WarmthLevel { get; set; }
    public int Stock { get; set; }
    public bool IsAvailable => Stock > 0;
    public List<ProductMeasurementDto> Measurements { get; set; } = new();
}

// Sub-DTO: Tabla de Medidas (Size Chart)
public class ProductMeasurementDto
{
    public string Name { get; set; } = string.Empty; // "Pecho"
    public decimal Value { get; set; } // 52.5
    public string Size { get; set; } = string.Empty; // "M"
    public string Unit { get; set; } = "cm";
}