using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApiCleanArchitectureDemo.Domain.Entities;

namespace MinimalApiCleanArchitectureDemo.Application.Features.Products.Commands;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (product == null)
        {
            return false;
        }

        product.Name = request.Name;
        product.Price = request.Price;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
