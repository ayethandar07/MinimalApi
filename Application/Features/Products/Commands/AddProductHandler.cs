﻿using MediatR;
using MinimalApiCleanArchitectureDemo.Domain.Entities;

namespace MinimalApiCleanArchitectureDemo.Application.Features.Products.Commands;

public class AddProductHandler : IRequestHandler<AddProductCommand, int>
{
    private readonly AppDbContext _context;

    public AddProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}