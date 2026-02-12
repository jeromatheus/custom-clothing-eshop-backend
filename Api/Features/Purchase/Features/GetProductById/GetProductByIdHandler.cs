using Api.Database;
using Api.Extensions;
using Api.Features.Purchase.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Purchase.Features.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdResult?>;
public record GetProductByIdResult(ProductDetailDto Product);

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResult?>
{
    private readonly AppDbContext _context;

    public GetProductByIdHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductByIdResult?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            // 1. Traer Grupos e Imágenes
            .Include(p => p.ImageGroups)
            .ThenInclude(g => g.Images)
            // 2. Traer Variantes y Talles
            .Include(p => p.Variants)
            .ThenInclude(p => p.Measurements)
            .AsSplitQuery() // Mejora performance cuando hay muchos Includes
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (product is null) return null;

        var dto = new ProductDetailDto
        {
            Id = product.Id,
            Name = product.Type.ToFriendlyName(),
            Sku = product.Sku,
            Price = product.Price,
            Type = product.Type.ToString(),
            ImageGroups = product.ImageGroups.Select(g => new ProductImageGroupDto
            {
                Images = g.Images.Select(i => i.ImageUrl).ToList()
            }).ToList(),

            Variants = product.Variants.Select(v => new ProductVariantDto
            {
                Id = v.Id,
                Color = v.Color.ToString(),
                Size = v.Size.ToString(),
                Stock = v.Stock,
                WarmthLevel = (int)v.WarmthLevel,
                Measurements = v.Measurements.Select(m => new ProductMeasurementDto
                {
                    Name = m.MeasurementName,
                    Value = m.Value,
                    Unit = m.Unit,
                    Size = m.Size.ToString() 
                }).ToList()
            }).ToList(),
        };

        return new GetProductByIdResult(dto);
    }
}