using MediatR;
using MinimalApiCleanArchitectureDemo.Application.Features.Products.Commands;
using MinimalApiCleanArchitectureDemo.Application.Features.Products.Queries;

namespace MinimalApiCleanArchitectureDemo.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var mediatr = app.ServiceProvider.GetRequiredService<IMediator>();

        // Define endpoints
        app.MapGet("/products", async (IMediator mediator) =>
            await mediator.Send(new GetProductsQuery()));

        app.MapPost("/products", async (IMediator mediator, AddProductCommand command) =>
            Results.Ok(await mediator.Send(command)));

        app.MapPut("/products/{id}", async (IMediator mediator, int id, UpdateProductCommand command) =>
        {
            if (id != command.Id)
            {
                return Results.BadRequest("Product ID mismatch.");
            }

            var success = await mediator.Send(command);

            if (!success)
            {
                return Results.NotFound("Product not found.");
            }

            return Results.Ok("Product updated successfully.");
        });
    }
}
