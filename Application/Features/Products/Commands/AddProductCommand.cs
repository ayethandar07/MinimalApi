using MediatR;
using MinimalApiCleanArchitectureDemo.Domain.Entities;

namespace MinimalApiCleanArchitectureDemo.Application.Features.Products.Commands;

public record AddProductCommand(string Name, decimal Price) : IRequest<Product>;
