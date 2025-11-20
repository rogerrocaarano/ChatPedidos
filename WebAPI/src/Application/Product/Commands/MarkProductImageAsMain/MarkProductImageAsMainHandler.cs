using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands.Handlers;

public class MarkProductImageAsMainHandler(IProductsRepository productsRepository)
    : ICommandHandler<MarkProductImageAsMainCommand>
{
    public async Task HandleAsync(
        MarkProductImageAsMainCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var product = await productsRepository.GetByIdAsync(message.ProductId, cancellationToken);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.MarkImageAsMain(message.ImageId);
        await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
