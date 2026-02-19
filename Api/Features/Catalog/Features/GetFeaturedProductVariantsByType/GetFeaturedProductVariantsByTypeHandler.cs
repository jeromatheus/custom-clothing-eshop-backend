using Api.Database;
using Api.Features.Catalog.Dtos;
using Api.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Catalog.Features.GetFeaturedProductVariantsByType;

public record GetFeaturedProductVariantsByTypeQuery(Garment ProductType)
    : IRequest<GetFeaturedProductVariantsByTypeResult>;

public record GetFeaturedProductVariantsByTypeResult(
    IEnumerable<FeaturedProductVariantDto> Products);

public class GetFeaturedProductVariantsByTypeHandler
    : IRequestHandler<GetFeaturedProductVariantsByTypeQuery, GetFeaturedProductVariantsByTypeResult>
{
    private readonly AppDbContext _context;

    public GetFeaturedProductVariantsByTypeHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetFeaturedProductVariantsByTypeResult> Handle(
        GetFeaturedProductVariantsByTypeQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Where(p => p.Garment == query.ProductType)
            .Select(p => new FeaturedProductVariantDto
            {
                Id = p.Variants
                    .Where(v => v.StockItems.Any(s => s.Quantity > 0))
                    .Select(v => v.Id.ToString())
                    .FirstOrDefault() ?? p.Variants.Select(v => v.Id.ToString()).FirstOrDefault()!,

                Name = p.GetFullName(),
                Price = p.Price,

                MainImageUrl = p.Variants
                    .OrderByDescending(v => v.StockItems.Any(s => s.Quantity > 0))
                    .SelectMany(v => v.ImageGroups)
                    .SelectMany(ig => ig.Images)
                    .OrderByDescending(i => i.IsMain)
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault() ?? "https://via.placeholder.com/150",

                Colors = p.Variants
                    .Where(v => v.StockItems.Any(s => s.Quantity > 0))
                    .Select(v => new ColorVariantSummaryDto
                    {
                        VariantId = v.Id.ToString(),
                        ColorName = v.Color.ToString(),
                        ImageUrl = v.ImageGroups
                            .SelectMany(ig => ig.Images)
                            .Where(i => i.IsMain)
                            .Select(i => i.ImageUrl)
                            .FirstOrDefault() ?? "https://via.placeholder.com/150"
                    }).ToList(),

                HasStock = p.Variants.Any(v => v.StockItems.Any(s => s.Quantity > 0))
            })
            .ToListAsync(cancellationToken);

        return new GetFeaturedProductVariantsByTypeResult(products);
    }
}