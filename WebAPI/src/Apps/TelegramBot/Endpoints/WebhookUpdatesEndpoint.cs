using Domain.Aggregates.Customer;
using FastEndpoints;
using TelegramBot.Adapters;
using TelegramBot.DTOs.Types;

namespace TelegramBot.Endpoints;

public class WebhookUpdatesEndpoint(TelegramProvider telegramProvider) : Endpoint<Update>
{
    private readonly TelegramProvider _telegramProvider = telegramProvider;

    public override void Configure()
    {
        Post("/updates/webhook");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Update req, CancellationToken ct)
    {
        var telegramId = new TelegramId(req.Message.From.Id);
        await _telegramProvider.SendMessageAsync(
            telegramId,
            "Your message has been received via webhook!"
        );

        await Send.OkAsync(cancellation: ct);
    }
}
