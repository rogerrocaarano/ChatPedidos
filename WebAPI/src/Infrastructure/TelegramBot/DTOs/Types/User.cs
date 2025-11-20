namespace TelegramBot.DTOs.Types;

public record class User(int Id, bool IsBot, string FirstName, string? LastName, string? Username);
