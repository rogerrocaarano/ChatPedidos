using System;
using Domain.Aggregates.Order;

namespace Domain.Repositories;

public interface IOrdersRepository : IRepository<Order> { }
