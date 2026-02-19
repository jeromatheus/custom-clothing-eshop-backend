using Api.Database;
using Api.Features.Purchase.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Purchase.Features.GetProductByAttributes;

public record GetUpdatedAttributesQuery(
    Garment? Garment,
    Neck? Neck,
    Fit? Fit,
    Material? Material,
    WarmthLevel? WarmthLevel
) : IRequest<GetUpdatedAttributesResult?>;

public record GetUpdatedAttributesResult(UpdatedAttributesDto Product);

public class GetUpdatedAttributesHandler
    : IRequestHandler<GetUpdatedAttributesQuery, GetUpdatedAttributesResult?>
{
    private readonly AppDbContext _context;

    public GetUpdatedAttributesHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetUpdatedAttributesResult?> Handle(
        GetUpdatedAttributesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.FixedAttributes.AsNoTracking();

        if (request.Garment.HasValue)
            query = query.Where(f => f.Garment == request.Garment.Value);

        if (request.Neck.HasValue)
            query = query.Where(f => f.Neck == request.Neck.Value);

        if (request.Fit.HasValue)
            query = query.Where(f => f.Fit == request.Fit.Value);

        if (request.Material.HasValue)
            query = query.Where(f => f.Material == request.Material.Value);

        if (request.WarmthLevel.HasValue)
            query = query.Where(f => f.WarmthLevel == request.WarmthLevel.Value);

        var product = await query
            .Select(f => new UpdatedAttributesDto
            {
                Id = f.Id,
                Name = $"{f.Garment} {f.Fit} {f.Material}",
                Price = f.Price,
                WarmthLevel = f.WarmthLevel.ToString(),

                SizeChart = new List<SizeSpecDto>(),    // TODO

                Variants = f.VariableAttributes
                    .Select(v => new ColorVariantDto
                    {
                        VariantId = v.Id,
                        ColorName = v.Color.ToString(),

                        Images = v.ImageGroups
                            .SelectMany(g => g.Images)
                            .OrderByDescending(i => i.IsMain)
                            .Select(i => i.ImageUrl)
                            .ToList(),

                        Sizes = v.StockItems
                            .OrderBy(s => s.Size)
                            .Select(s => new SizeStockDto
                            {
                                Size = s.Size.ToString(),
                                Stock = s.Quantity
                            })
                            .ToList()
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
            return null;

        return new GetUpdatedAttributesResult(product);
    }
}