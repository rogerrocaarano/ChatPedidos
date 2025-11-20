namespace TelegramBot.DTOs.Types;

public record class Update(int UpdateId, Message? Message, Message? EditedMessage);
