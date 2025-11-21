using System;
using Domain.ValueObjects;
using LiteBus.Commands.Abstractions;

namespace Application.Order.Commands.StartOrderFlowFromTelegram;

public sealed record StartOrderFlowFromTelegramCommand(TelegramId TelegramId, string CustomerName)
    : ICommand<Guid>;
