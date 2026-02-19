using Api.Features.Catalog.Dtos; 
using Carter;
using MediatR;

namespace Api.Features.Catalog.Features.GetProductVariantDetailById;

public record GetProductVariantDetailByIdResponse(ProductVariantDetailDto Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}",
            async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductVariantDetailByIdQuery(id));

                if (result.Product is null)
                {
                    return Results.NotFound(new { message = "Producto no encontrado." });
                }

                return Results.Ok(new GetProductVariantDetailByIdResponse(result.Product));
            })
        .WithName("GetProductById")
        .Produces<GetProductVariantDetailByIdResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithTags("Catalog")
        .WithSummary("Obtener los detalles completos de un producto por Id y Color");
    }
}