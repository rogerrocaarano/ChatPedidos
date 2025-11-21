using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands.RemoveProductImage;

public class RemoveProductImageHandler(IProductsRepository productsRepository)
    : ICommandHandler<RemoveProductImageCommand>
{
    public async Task HandleAsync(
        RemoveProductImageCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var product = await productsRepository.GetByIdAsync(message.ProductId, cancellationToken);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.RemoveImage(message.ImageId);
        await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
