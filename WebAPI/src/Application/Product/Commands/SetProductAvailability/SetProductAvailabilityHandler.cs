using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands.SetProductAvailability;

public class SetProductAvailabilityHandler(IProductsRepository productsRepository)
    : ICommandHandler<SetProductAvailabilityCommand>
{
    public async Task HandleAsync(
        SetProductAvailabilityCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var product = await productsRepository.GetByIdAsync(message.ProductId, cancellationToken);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        if (message.IsAvailable)
        {
            product.SetProductAsAvailable();
        }
        else
        {
            product.SetProductAsUnavailable();
        }

        await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
