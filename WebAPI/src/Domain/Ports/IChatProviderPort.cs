namespace Domain.Ports;

public interface IChatProviderPort<TId>
    where TId : IValueObject
{
    Task SendMessageAsync(TId toId, string message);
    Task SendWelcomeMessageAsync(TId toId);
    Task SendProductCatalogAsync(TId toId);
}
