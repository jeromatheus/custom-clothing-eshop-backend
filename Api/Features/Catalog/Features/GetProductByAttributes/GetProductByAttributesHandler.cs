using Api.Database;
using Api.Features.Catalog.Dtos;
using Api.Models.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.Catalog.Features.GetFeaturedProductsByType;

// Recibe los Enums nulables desde la URL
public record GetProductByAttributesQuery(
    Garment? Garment,
    Neck? Neck,
    Fit? Fit,
    Material? Material,
    Warmth? Warmth
) : IRequest<GetProductByAttributesResult>;

public record GetProductByAttributesResult(ProductVariantDetailDto? Product);

public class GetProductByAttributesHandler
    : IRequestHandler<GetProductByAttributesQuery, GetProductByAttributesResult>
{
    private readonly AppDbContext _context;

    public GetProductByAttributesHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductByAttributesResult> Handle(
        GetProductByAttributesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Products.AsNoTracking();

        // 1. Aplicamos los filtros seleccionados en el formulario
        if (request.Garment.HasValue) query = query.Where(p => p.Garment == request.Garment.Value);
        if (request.Neck.HasValue) query = query.Where(p => p.Neck == request.Neck.Value);
        if (request.Fit.HasValue) query = query.Where(p => p.Fit == request.Fit.Value);
        if (request.Material.HasValue) query = query.Where(p => p.Material == request.Material.Value);
        if (request.Warmth.HasValue) query = query.Where(p => p.Warmth == request.Warmth.Value);

        // 2. Proyectamos todo el árbol de relaciones directamente al DTO
        var product = await query
            .Select(p => new ProductVariantDetailDto
            {
                Id = p.Id,
                Name = p.GetFullName(),
                Price = p.Price,
                WarmthLevel = p.Warmth == Warmth.Low ? 1 :
                              p.Warmth == Warmth.Medium ? 2 :
                              p.Warmth == Warmth.High ? 3 : 0,

                // A. Guía de Talles específica para este corte/producto
                SizeChart = p.SizeMeasurements
                    .OrderBy(sm => sm.Size)
                    .Select(sm => new SizeSpecDto
                    {
                        Size = sm.Size.ToString(),
                        Chest = sm.Chest,
                        Length = sm.Length,
                        Neck = sm.Neck
                    }).ToList(),

                // B. Variantes de Colores y sus talles en stock
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

                // C. Modelos y Fotos para el Carrusel
                Models = p.Variants
                    .SelectMany(v => v.ImageGroups)
                    .Select(ig => new ModelDto
                    {
                        Id = ig.Id.ToString(),
                        Name = "Modelo " + ig.ModelWearingSize,
                        HeightInfo = ig.ModelHeight.ToString() ?? "-",
                        SizeInfo = ig.ModelWearingSize.ToString() ?? "-",

                        // Buscamos la imagen marcada como Principal para la miniatura del selector
                        ImageUrl = ig.Images
                            .Where(i => i.IsMain)
                            .Select(i => i.ImageUrl)
                            .FirstOrDefault() ?? "https://via.placeholder.com/90x140",

                        // Todas las imágenes de este grupo ordenadas para el carrusel
                        CarouselImages = ig.Images
                            .OrderByDescending(i => i.IsMain)
                            .Select(i => i.ImageUrl)
                            .ToList()
                    }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return new GetProductByAttributesResult(product);
    }
}