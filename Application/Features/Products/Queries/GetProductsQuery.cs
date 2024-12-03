using MediatR;
using MinimalApiCleanArchitectureDemo.Domain.Entities;

namespace MinimalApiCleanArchitectureDemo.Application.Features.Products.Queries;

public record GetProductsQuery() : IRequest<List<Product>>;
