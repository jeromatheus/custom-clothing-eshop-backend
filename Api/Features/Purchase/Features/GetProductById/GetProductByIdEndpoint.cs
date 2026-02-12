using Carter;
using MediatR;

namespace Api.Features.Purchase.Features.GetProductById;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));

            if (result is null)
                return Results.NotFound(new { message = "Producto no encontrado" });

            return Results.Ok(result.Product);
        })
        .WithName("GetProductById")
        .WithTags("Catalog");
    }
}