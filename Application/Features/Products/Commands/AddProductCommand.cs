using MediatR;

namespace MinimalApiCleanArchitectureDemo.Application.Features.Products.Commands;

public record AddProductCommand(string Name, decimal Price) : IRequest<int>;
