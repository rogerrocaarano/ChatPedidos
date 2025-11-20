using Domain.Aggregates.Customer;
using Domain.ValueObjects;

namespace Domain.Repositories;

public interface ICustomersRepository : IRepository<Customer>
{
    Task<Customer?> GetByTelegramIdAsync(
        TelegramId telegramId,
        CancellationToken cancellationToken = default
    );
}
