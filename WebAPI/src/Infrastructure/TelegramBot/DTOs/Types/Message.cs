namespace TelegramBot.DTOs.Types;

public record class Message(int MessageId, User From, int Date, Chat Chat, string Text);
