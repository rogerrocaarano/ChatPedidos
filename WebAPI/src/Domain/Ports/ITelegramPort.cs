using System;
using Domain.ValueObjects;

namespace Domain.Ports;

public interface ITelegramPort
{
    Task SendMessageAsync(TelegramId id, string message);
    Task SendMessageWithOptionsAsync(TelegramId id, string message, ICollection<BotOption> options);
}
