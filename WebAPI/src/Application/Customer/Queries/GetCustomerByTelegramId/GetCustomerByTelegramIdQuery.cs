using Application.Customer.Queries.GetCustomerByTelegramId;
using Domain.ValueObjects;
using LiteBus.Queries.Abstractions;

namespace Application.Customer.Queries.GetCustomerByTelegramId;

public sealed record GetCustomerByTelegramIdQuery(TelegramId TelegramId) : IQuery<CustomerDto?>;