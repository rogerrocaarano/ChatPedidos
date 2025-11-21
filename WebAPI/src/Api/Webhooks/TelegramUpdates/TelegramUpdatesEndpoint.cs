using System.Threading.Tasks;
using Application.Product.Queries.GetProductsListForTelegram;
using Domain.ValueObjects;
using FastEndpoints;
using LiteBus.Queries.Abstractions;
using TelegramBot.Adapters;
using TelegramBot.Consts;
using TelegramBot.DTOs.Types;

namespace Api.Webhooks.TelegramUpdates;

public class TelegramUpdatesEndpoint(
    TelegramProvider telegramProvider,
    IQueryMediator queryMediator
) : Endpoint<Update>
{
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
            ReplyMarkupCommands.CustomerRequestsMenu => HandleCustomerRequestsMenu(telegramId),
            _ => telegramProvider.SendMessageAsync(telegramId, BotMessages.GenericError),
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

        await telegramProvider.SendMessageWithOptionsAsync(
            telegramId,
            BotMessages.Welcome,
            options
        );
    }

    private async Task HandleCustomerRequestsLocation(TelegramId telegramId)
    {
        var str = BotMessages.WorkingHours;
        await telegramProvider.SendMessageAsync(telegramId, str);
    }

    private async Task HandleCustomerRequestsMenu(TelegramId telegramId)
    {
        var products = await queryMediator.QueryAsync(new GetProductListForTelegramQuery());
        // send to telegramId the list of products
        var message = "Aquí está nuestro menú de productos:\n\n";
        foreach (var product in products)
        {
            message += $"<b>{product.Name}</b>\n";
            message += $"{product.Description}\n";
            message += $"<b>Precio: Bs. {product.Price}</b>\n\n";
        }
        await telegramProvider.SendMessageAsync(telegramId, message);
    }
}
