using TelegramBot.Adapters;

namespace TelegramBot.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddTelegramBot(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var botToken = configuration["Telegram:Token"];
        if (string.IsNullOrEmpty(botToken))
        {
            throw new InvalidOperationException("Telegram:Token is not configured.");
        }
        // Configure RestClient for Telegram API
        services.AddSingleton(sp => RestClientConfiguration.ConfigureTelegramRestClient(botToken));

        // Register TelegramProvider
        services.AddSingleton<TelegramProvider>();

        return services;
    }
}
