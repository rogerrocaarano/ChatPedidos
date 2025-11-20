using System.Text.Json;
using RestSharp;
using RestSharp.Serializers.Json;

namespace TelegramBot.Configurations;

public static class RestClientConfiguration
{
    public static RestClient ConfigureTelegramRestClient(string token)
    {
        var options = new RestClientOptions
        {
            BaseUrl = new Uri($"https://api.telegram.org/bot{token}/"),
        };

        var serializer = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            PropertyNameCaseInsensitive = true,
        };

        var client = new RestClient(
            options,
            configureSerialization: s =>
                s.UseSerializer(() => new SystemTextJsonSerializer(serializer))
        );

        return client;
    }
}
