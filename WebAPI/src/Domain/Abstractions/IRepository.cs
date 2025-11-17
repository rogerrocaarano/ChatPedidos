using System;

namespace Domain.Abstractions;

public interface IRepository<T>
    where T : IAggregateRoot
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(T aggregate);
    void Remove(T aggregate);
}
