using Domain.Ports;
using Domain.ValueObjects;
using RestSharp;
using TelegramBot.DTOs.Requests;
using TelegramBot.DTOs.Types;
using TelegramBot.Interaction;

namespace TelegramBot.Adapters;

public class TelegramProvider(RestClient apiClient) : ITelegramPort
{
    private readonly RestClient _apiClient = apiClient;

    public async Task SendMessageAsync(TelegramId id, string message)
    {
        var body = SendMessageRequest.Create(id.Id, message);
        var request = new RestRequest("sendMessage").AddJsonBody(body);
        var response = await _apiClient.ExecutePostAsync<Message>(request);
        if (!response.IsSuccessful)
        {
            throw new Exception(
                $"Failed to send message to Telegram ID {id.Id}. Response: {response.Content}"
            );
        }
    }

    public async Task SendMessageWithOptionsAsync(
        TelegramId id,
        string message,
        ICollection<BotOption> options
    )
    {
        var buttons = options.Select(option => KeyboardButton.WithText(option.Value)).ToList();
        var keyboard = ReplyKeyboardMarkup.Create(buttons);
        var body = SendMessageRequest.Create(id.Id, message).WithReplyMarkup(keyboard);
        var request = new RestRequest("sendMessage").AddJsonBody(body);
        var response = await _apiClient.ExecutePostAsync<Message>(request);
        if (!response.IsSuccessful)
        {
            throw new Exception(
                $"Failed to send message with options to Telegram ID {id.Id}. Response: {response.Content}"
            );
        }
    }
}
