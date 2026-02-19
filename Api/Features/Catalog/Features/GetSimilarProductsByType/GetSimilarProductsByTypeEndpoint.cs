using Api.Features.Catalog.Dtos;
using Api.Model;
using Carter;
using MediatR;

namespace Api.Features.Catalog.Features.GetSimilarProductsByType;

public record GetSimilarProductsByTypeResponse(
    IEnumerable<SimilarProductDto> Products);

public class GetSimilarProductsByTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/type/{productType}",
            async (Garment productType, ISender sender) =>
            {
                var query = new GetSimilarProductsByTypeQuery(productType);

                var result = await sender.Send(query);

                return Results.Ok(new GetSimilarProductsByTypeResponse(result.Products));
            })
        .WithName("GetProductsByType")
        .Produces<GetSimilarProductsByTypeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithTags("Catalog")
        .WithSummary("Obtener productos por tipo de prenda");
    }
}
