using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Commands.Handlers;

public class UpdateProductPriceHandler(IProductsRepository productsRepository)
    : ICommandHandler<UpdateProductPriceCommand>
{
    public async Task HandleAsync(
        UpdateProductPriceCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var product = await productsRepository.GetByIdAsync(message.ProductId, cancellationToken);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.UpdatePrice(message.Price);
        await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
