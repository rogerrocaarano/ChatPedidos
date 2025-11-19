using System;
using Domain.Aggregates.Order;

namespace Domain.Repositories;

public interface IOrdersRepository : IRepository<Order>
{
    Task<ICollection<Order>> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken = default
    );
}
