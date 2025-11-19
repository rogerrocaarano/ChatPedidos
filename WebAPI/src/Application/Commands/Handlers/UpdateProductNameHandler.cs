using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Commands.Handlers;

public class UpdateProductNameHandler(IProductsRepository productsRepository)
    : ICommandHandler<UpdateProductNameCommand>
{
    public async Task HandleAsync(
        UpdateProductNameCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var product = await productsRepository.GetByIdAsync(message.ProductId, cancellationToken);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.UpdateName(message.Name);
        await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
