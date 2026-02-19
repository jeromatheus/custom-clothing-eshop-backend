using Api.Database;
using Api.Features.Catalog.Dtos;
using Api.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Catalog.Features.GetProductVariantDetailById;

public record GetProductVariantDetailByIdQuery(Guid Id) : IRequest<GetProductVariantDetailByIdResult>;

public record GetProductVariantDetailByIdResult(ProductVariantDetailDto? Product);

public class GetProductVariantDetailByIdHandler
    : IRequestHandler<GetProductVariantDetailByIdQuery, GetProductVariantDetailByIdResult>
{
    private readonly AppDbContext _context;

    public GetProductVariantDetailByIdHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductVariantDetailByIdResult> Handle(
        GetProductVariantDetailByIdQuery request,
        CancellationToken cancellationToken)
    {
        var initialVariant = await _context.Variants
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

        if (initialVariant == null) return new GetProductVariantDetailByIdResult(null);

        var productDetail = await _context.Products
            .AsNoTracking()
            .Where(p => p.Id == initialVariant.ProductId)
            .Select(p => new ProductVariantDetailDto
            {
                Id = initialVariant.Id,
                Name = p.Garment + " " + p.Neck + " " + p.Fit + " " + p.Material,
                Price = p.Price,

                GarmentType = p.Garment.ToString(),
                NeckType = p.Neck.ToString(),
                FitType = p.Fit.ToString(),
                MaterialType = p.Material.ToString(),

                WarmthLevel = p.Warmth == Warmth.Low ? 1 :
                              p.Warmth == Warmth.Medium ? 2 :
                              p.Warmth == Warmth.High ? 3 : 0,

                SizeChart = p.SizeMeasurements
                    .OrderBy(sm => sm.Size)
                    .Select(sm => new SizeSpecDto
                    {
                        Size = sm.Size.ToString(),
                        Chest = sm.Chest,
                        Length = sm.Length,
                        Neck = sm.Neck
                    }).ToList(),

                Variants = p.Variants
                    .Select(v => new ColorVariantDto
                    {
                        VariantId = v.Id,
                        ColorName = v.Color.ToString(),
                        Sizes = v.StockItems
                            .OrderBy(si => si.Size)
                            .Select(si => new SizeStockDto
                            {
                                Size = si.Size.ToString(),
                                Stock = si.Quantity
                            }).ToList()
                    }).ToList(),

                Models = p.Variants
                    .Where(v => v.Id == initialVariant.Id) 
                    .SelectMany(v => v.ImageGroups)
                    .Select(ig => new ModelDto
                    {
                        Id = ig.Id.ToString(),
                        ColorName = ig.Variant.Color.ToString(),
                        Name = "Modelo " + ig.ModelWearingSize,
                        HeightInfo = ig.ModelHeight.ToString() ?? "-",
                        SizeInfo = ig.ModelWearingSize.ToString() ?? "-",
                        ImageUrl = ig.Images
                            .Where(i => i.IsMain)
                            .Select(i => i.ImageUrl)
                            .FirstOrDefault() ?? "https://via.placeholder.com/90x140",
                        CarouselImages = ig.Images
                            .OrderByDescending(i => i.IsMain)
                            .Select(i => i.ImageUrl)
                            .ToList()
                    }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return new GetProductVariantDetailByIdResult(productDetail);
    }
}