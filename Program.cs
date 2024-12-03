using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApiCleanArchitectureDemo.Application.Features.Products.Commands;
using MinimalApiCleanArchitectureDemo.Application.Features.Products.Queries;
using MinimalApiCleanArchitectureDemo.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=products.db"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migrate database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Define endpoints
app.MapGet("/products", async (IMediator mediator) =>
    await mediator.Send(new GetProductsQuery()));

app.MapPost("/products", async (IMediator mediator, AddProductCommand command) =>
    Results.Ok(await mediator.Send(command)));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
