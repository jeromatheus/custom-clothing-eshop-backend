using Api.Features.Catalog.Dtos;
using Carter;
using Mapster;
using MediatR;

namespace Api.Features.Catalog.Features.GetSimilarProductsByType;

public record GetSimilarProductsByTypeResponse(IEnumerable<ProductDto> Products);

public class GetSimilarProductsByTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/type/{productType}", async (string productType, ISender sender) =>
        {
            var query = new GetSimilarProductsByTypeQuery(productType);

            var result = await sender.Send(query);

            var response = result.Adapt<GetSimilarProductsByTypeResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductsByType")
        .Produces<GetSimilarProductsByTypeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithTags("Catalog") // Agrupar en Swagger
        .WithSummary("Obtener productos por categoría");
    }
}