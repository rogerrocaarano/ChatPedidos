using System.Threading.Tasks;
using Domain.ValueObjects;
using FastEndpoints;
using TelegramBot.Adapters;
using TelegramBot.Consts;
using TelegramBot.DTOs.Types;

namespace Api.Webhooks.TelegramUpdates;

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

        var commandResult = req.Message.Text switch
        {
            "/start" => HandleStartCommand(telegramId),
            ReplyMarkupCommands.CustomerRequestsLocation => HandleCustomerRequestsLocation(
                telegramId
            ),
            _ => _telegramProvider.SendMessageAsync(telegramId, BotMessages.GenericError),
        };

        await commandResult;
        await Send.OkAsync(cancellation: ct);
    }

    private async Task HandleStartCommand(TelegramId telegramId)
    {
        // hacer una lista con todos los elementos de ReplyMarkupCommands
        var options = new List<BotOption>
        {
            new(ReplyMarkupCommands.CustomerStartsOrderFlow),
            new(ReplyMarkupCommands.CustomerRequestsLocation),
            new(ReplyMarkupCommands.CustomerRequestsMenu),
            new(ReplyMarkupCommands.CustomerWhantsToTalkToHuman),
        };

        await _telegramProvider.SendMessageWithOptionsAsync(
            telegramId,
            BotMessages.Welcome,
            options
        );
    }

    private async Task HandleCustomerRequestsLocation(TelegramId telegramId)
    {
        var str = BotMessages.WorkingHours;
        await _telegramProvider.SendMessageAsync(telegramId, str);
    }
}
