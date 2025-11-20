using Domain.Aggregates.Customer;
using Domain.Ports;
using RestSharp;
using TelegramBot.DTOs.Requests;
using TelegramBot.DTOs.Types;
using TelegramBot.Interaction;

namespace TelegramBot.Adapters;

public class TelegramProvider(RestClient apiClient) : IChatProviderPort<TelegramId>
{
    private readonly RestClient _apiClient = apiClient;

    public async Task SendMessageAsync(TelegramId toId, string message)
    {
        var body = SendMessageRequest.Create(toId.Id, message);
        var request = new RestRequest("sendMessage").AddJsonBody(body);
        await _apiClient.PostAsync<Message>(request);
    }

    public Task SendProductCatalogAsync(TelegramId toId)
    {
        throw new NotImplementedException();
    }

    public async Task SendWelcomeMessageAsync(TelegramId toId)
    {
        var keyboard = ReplyKeyboardMarkup.Create(
            [
                KeyboardButton.WithText("Hacer un pedido"),
                KeyboardButton.WithText("Horarios de atención"),
            ]
        );
        var message = "¡Bienvenido! ¿En qué puedo ayudarte hoy?";
        var body = SendMessageRequest.Create(toId.Id, message).WithReplyMarkup(keyboard);
        var request = new RestRequest("sendMessage").AddJsonBody(body);
        var response = await _apiClient.ExecutePostAsync<Message>(request);
        if (!response.IsSuccessful)
        {
            await SendMessageAsync(
                toId,
                response.Content ?? "Error al enviar el mensaje de bienvenida."
            );
        }
    }
}
