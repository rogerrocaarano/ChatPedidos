using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands.Handlers;

public class CreateProductHandler(IProductsRepository productsRepository)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> HandleAsync(
        CreateProductCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var product = global::Domain
            .Aggregates.Product.Product.Create(message.Name)
            .WithDescription(message.Description)
            .WithPrice(message.Price);

        productsRepository.Add(product);
        await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
