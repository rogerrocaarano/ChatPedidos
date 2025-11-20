using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Product.Commands.UpdateProductDescription;

public class UpdateProductDescriptionHandler(IProductsRepository productsRepository)
    : ICommandHandler<UpdateProductDescriptionCommand>
{
    public async Task HandleAsync(
        UpdateProductDescriptionCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var product = await productsRepository.GetByIdAsync(message.ProductId, cancellationToken);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.UpdateDescription(message.Description);
        await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
