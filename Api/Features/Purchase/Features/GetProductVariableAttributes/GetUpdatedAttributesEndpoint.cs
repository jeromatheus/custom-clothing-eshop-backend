using Carter;
using MediatR;

namespace Api.Features.Purchase.Features.GetProductByAttributes;

public class GetUpdatedAttributesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/attributes",
            async ([AsParameters] GetUpdatedAttributesQuery query, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(query, ct);

                if (result is null)
                    return Results.NotFound(new
                    {
                        message = "No se encontró ningún producto con esa combinación de atributos."
                    });

                return Results.Ok(result.Product);
            })
        .WithName("GetProductByAttributes")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithTags("Catalog")
        .WithSummary("Obtiene variantes, precio y fotos filtrando por atributos fijos");
    }
}