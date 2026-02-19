using Api.Database;
using Api.Features.Catalog.Dtos;
using Api.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Catalog.Features.GetSimilarProductsByType;

public record GetSimilarProductsByTypeQuery(Garment ProductType)
    : IRequest<GetSimilarProductsByTypeResult>;

public record GetSimilarProductsByTypeResult(
    IEnumerable<SimilarProductDto> Products);

public class GetSimilarProductsByTypeHandler
    : IRequestHandler<GetSimilarProductsByTypeQuery, GetSimilarProductsByTypeResult>
{
    private readonly AppDbContext _context;

    public GetSimilarProductsByTypeHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetSimilarProductsByTypeResult> Handle(
        GetSimilarProductsByTypeQuery query,
        CancellationToken cancellationToken)
    {
        var products = await _context.FixedAttributes
            .AsNoTracking()
            .Where(f => f.Garment == query.ProductType)
            .Select(f => new SimilarProductDto
            {
                Sku = f.Id.ToString(), // TODO
                Name = f.GetFullName(),
                Price = f.Price,

                MainImageUrl = f.VariableAttributes
                    .SelectMany(v => v.ImageGroups)
                    .SelectMany(g => g.Images)
                    .OrderByDescending(i => i.IsMain)
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault()
                        ?? "https://via.placeholder.com/150",

                AvailableColors = f.VariableAttributes
                    .Where(v => v.StockItems.Any(s => s.Quantity > 0))
                    .Select(v => v.Color.ToString())
                    .Distinct()
                    .ToList(),

                HasStock = f.VariableAttributes
                    .Any(v => v.StockItems.Any(s => s.Quantity > 0))
            })
            .ToListAsync(cancellationToken);

        return new GetSimilarProductsByTypeResult(products);
    }
}
