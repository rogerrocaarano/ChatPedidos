using Domain.Repositories;
using LiteBus.Commands.Abstractions;

namespace Application.Commands.Handlers;

public class AddProductImageHandler(IProductsRepository productsRepository)
    : ICommandHandler<AddProductImageCommand>
{
    public async Task HandleAsync(
        AddProductImageCommand message,
        CancellationToken cancellationToken = default
    )
    {
        var product = await productsRepository.GetByIdAsync(message.ProductId, cancellationToken);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        product.AddImage(message.ImageUrl, message.SetAsMain);
        await productsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
