namespace TelegramBot.DTOs.Requests;

public record class SendMessageRequest(long ChatId, string Text);
