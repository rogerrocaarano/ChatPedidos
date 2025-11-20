using Domain.Aggregates.Customer;
using Domain.Ports;
using RestSharp;
using TelegramBot.DTOs.Requests;
using TelegramBot.DTOs.Types;

namespace TelegramBot.Adapters;

public class TelegramProvider(RestClient apiClient) : IChatProviderPort<TelegramId>
{
    private readonly RestClient _apiClient = apiClient;

    public async Task SendMessageAsync(TelegramId toId, string message)
    {
        var body = new SendMessageRequest(toId.Id, message);
        var request = new RestRequest("sendMessage").AddJsonBody(body);
        await _apiClient.PostAsync<Message>(request);
    }
}
