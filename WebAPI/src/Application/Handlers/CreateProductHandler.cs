using Application.Commands;
using Domain.Aggregates.Product;
using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Handlers;

public class CreateProductHandler(IProductsRepository productsRepository)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> HandleAsync(
        CreateProductCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var product = Product
            .Create(message.Name)
            .WithDescription(message.Description)
            .WithPrice(message.Price);

        productsRepository.Add(product);
        await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
