namespace Api.Features.Catalog.Dtos;

// ==============================================================================
// FEATURE: 
// Endpoint: 
// Uso: 
// ==============================================================================
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }

    // Mapea a <WarmthLevelBadge level={warmthLevel} />
    // Debes convertir tu Enum Warmth a int (1, 2 o 3)
    public int WarmthLevel { get; set; }
    
    public List<ModelDto> Models { get; set; } = new();
    public ProductConfigDto FormConfig { get; set; } = new();
    
    // (Opcional) Si quieres mandarle al frontend cuáles fueron los filtros
    // que originaron esta respuesta para que el formulario los marque como selected.
    public Dictionary<string, string> SelectedAttributes { get; set; } = new();
}


public class ProductConfigDto
{
    public List<FilterGroupDto> Groups { get; set; } = new();
}

public class FilterGroupDto
{
    public string Id { get; set; } = string.Empty; // "color", "size", "fit"
    public string Label { get; set; } = string.Empty; // "Color", "Talle", "Corte"
    public string Type { get; set; } = string.Empty;
    public List<FilterOptionDto> Options { get; set; } = new();
}

public class FilterOptionDto
{
    public string Value { get; set; } = string.Empty; // El valor real del Enum (Ej: "Slim")
    public string Label { get; set; } = string.Empty; // Lo que ve el usuario (Ej: "Ajustado")
    public string? Hex { get; set; }
}