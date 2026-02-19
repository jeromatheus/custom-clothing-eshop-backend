using Api.Features.Catalog.Dtos;
using Carter;
using MediatR;

namespace Api.Features.Catalog.Features.GetFeaturedProductsByType;

public class GetProductByAttributesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/attributes",
            async ([AsParameters] GetProductByAttributesQuery query, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(query, ct);

                if (result.Product is null)
                {
                    return Results.NotFound(new { message = "Esta combinación no existe o no está disponible." });
                }

                return Results.Ok(result.Product);
            })
        .WithName("GetProductByAttributes")
        .Produces<ProductVariantDetailDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Catalog")
        .WithSummary("Obtener detalles, stock y fotos por combinación de atributos");
    }
}