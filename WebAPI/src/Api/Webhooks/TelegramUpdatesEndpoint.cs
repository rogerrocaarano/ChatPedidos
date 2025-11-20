using Domain.ValueObjects;
using FastEndpoints;
using TelegramBot.Adapters;
using TelegramBot.DTOs.Types;

namespace Api.Webhooks;

public class TelegramUpdatesEndpoint(TelegramProvider telegramProvider) : Endpoint<Update>
{
    private readonly TelegramProvider _telegramProvider = telegramProvider;

    public override void Configure()
    {
        Post("/webhooks/telegram-updates");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Update req, CancellationToken ct)
    {
        var telegramId = new TelegramId(req.Message.From.Id);
        await _telegramProvider.SendWelcomeMessageAsync(telegramId);

        await Send.OkAsync(cancellation: ct);
    }
}
