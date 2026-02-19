using Api.Features.Catalog.Dtos;
using Api.Models.Enums;
using Carter;
using MediatR;

namespace Api.Features.Catalog.Features.GetFeaturedProductVariantsByType;

public record GetFeaturedProductVariantsByTypeResponse(
    IEnumerable<FeaturedProductVariantDto> Products);

public class GetFeaturedProductVariantsByTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/type/{productType}",
            async (Garment productType, ISender sender) =>
            {
                var query = new GetFeaturedProductVariantsByTypeQuery(productType);

                var result = await sender.Send(query);

                return Results.Ok(new GetFeaturedProductVariantsByTypeResponse(result.Products));
            })
        .WithName("GetProductsByType")
        .Produces<GetFeaturedProductVariantsByTypeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithTags("Catalog")
        .WithSummary("Obtener productos por tipo de prenda");
    }
}