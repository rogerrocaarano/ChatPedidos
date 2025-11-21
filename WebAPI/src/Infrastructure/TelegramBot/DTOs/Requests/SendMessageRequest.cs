using TelegramBot.Interaction;

namespace TelegramBot.DTOs.Requests;

public class SendMessageRequest
{
    public long ChatId { get; private set; }
    public string Text { get; private set; }
    public ReplyKeyboardMarkup? ReplyMarkup { get; private set; }
    public string ParseMode { get; private set; }

    private SendMessageRequest(long chatId, string text)
    {
        ChatId = chatId;
        Text = text;
        ReplyMarkup = null;
        ParseMode = "HTML";
    }

    public static SendMessageRequest Create(long chatId, string text)
    {
        return new SendMessageRequest(chatId, text);
    }

    public SendMessageRequest WithReplyMarkup(ReplyKeyboardMarkup markup)
    {
        ReplyMarkup = markup;
        return this;
    }
}
