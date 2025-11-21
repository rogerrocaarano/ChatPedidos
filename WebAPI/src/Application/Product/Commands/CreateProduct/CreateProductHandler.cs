using Domain.Repositories;
using LiteBus.Commands.Abstractions;
using Aggregate = Domain.Aggregates.Product;

namespace Application.Product.Commands.CreateProduct;

public class CreateProductHandler(IProductsRepository productsRepository)
    : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> HandleAsync(
        CreateProductCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var product = Aggregate
            .Product.Create(message.Name)
            .WithDescription(message.Description)
            .WithPrice(message.Price);

        productsRepository.Add(product);
        await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
