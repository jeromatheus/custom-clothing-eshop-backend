using Api.Database;
using Api.Extensions;
using Api.Features.Catalog.Dtos;
using Api.Features.Purchase.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Catalog.Features.GetSimilarProductsByType;

public record GetSimilarProductsByTypeQuery(string ProductType) : IRequest<GetSimilarProductsByTypeResult>;
public record GetSimilarProductsByTypeResult(IEnumerable<ProductDto> Products);

public class GetSimilarProductsByTypeHandler : IRequestHandler<GetSimilarProductsByTypeQuery, GetSimilarProductsByTypeResult>
{
    private readonly AppDbContext _context;

    public GetSimilarProductsByTypeHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetSimilarProductsByTypeResult> Handle(GetSimilarProductsByTypeQuery query, CancellationToken cancellationToken)
    {
        // 1. Validación del Enum
        if (!Enum.TryParse<ProductType>(query.ProductType, true, out var typeEnum))
        {
            throw new ArgumentException($"El tipo '{query.ProductType}' no es válido.");
        }

        // 2. Consulta a BD (AGREGAMOS .Include(Variants))
        var products = await _context.Products
            .Include(p => p.Variants)
            .Include(p => p.ImageGroups)
            .ThenInclude(g => g.Images)
            .Include(p => p.Variants) 
            .Where(p => p.Type == typeEnum)
            .ToListAsync(cancellationToken);

        // 3. Mapeo
        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Type.ToFriendlyName(),
            Sku = p.Sku,
            Price = p.Price,

            MainImageUrl = p.ImageGroups
                .SelectMany(g => g.Images)
                .OrderByDescending(i => i.IsMain)
                .FirstOrDefault()?.ImageUrl
                ?? "https://via.placeholder.com/150",

            AvailableColors = p.Variants
                .Where(v => v.Stock > 0)
                .Select(v => v.Color.ToString())
                .Distinct() // Elimina los talles implícitamente (Rojo S y Rojo M)
                .ToList(),

            HasStock = p.Variants.Any(v => v.Stock > 0) // TODO: cómo manejo si un color si tiene stock pero otro no
        });

        return new GetSimilarProductsByTypeResult(productDtos);
    }
}