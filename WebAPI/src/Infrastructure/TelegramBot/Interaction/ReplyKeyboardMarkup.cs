using System;

namespace TelegramBot.Interaction;

public class ReplyKeyboardMarkup
{
    public ICollection<ICollection<KeyboardButton>> Keyboard { get; private set; }
    public bool IsPersistent { get; private set; }
    public bool ResizeKeyboard { get; private set; }
    public bool OneTimeKeyboard { get; private set; }
    public string? InputFieldPlaceholder { get; private set; }
    public bool Selective { get; private set; }

    private ReplyKeyboardMarkup()
    {
        Keyboard = new List<ICollection<KeyboardButton>>();
        IsPersistent = false;
        ResizeKeyboard = false;
        OneTimeKeyboard = false;
        InputFieldPlaceholder = null;
        Selective = false;
    }

    public static ReplyKeyboardMarkup Create(IEnumerable<KeyboardButton> keyboard)
    {
        var markup = new ReplyKeyboardMarkup
        {
            Keyboard = [.. keyboard.Select(kb => (ICollection<KeyboardButton>)[kb])],
        };
        return markup;
    }

    public ReplyKeyboardMarkup WithPersistent(bool isPersistent = true)
    {
        IsPersistent = isPersistent;
        return this;
    }

    public ReplyKeyboardMarkup WithResizeKeyboard(bool resizeKeyboard = true)
    {
        ResizeKeyboard = resizeKeyboard;
        return this;
    }

    public ReplyKeyboardMarkup WithOneTimeKeyboard(bool oneTimeKeyboard = true)
    {
        OneTimeKeyboard = oneTimeKeyboard;
        return this;
    }

    public ReplyKeyboardMarkup WithInputFieldPlaceholder(string placeholder)
    {
        if (placeholder.Length < 1 || placeholder.Length > 64)
            throw new ArgumentException(
                "Input field placeholder must be between 1 and 64 characters",
                nameof(placeholder)
            );

        InputFieldPlaceholder = placeholder;
        return this;
    }

    public ReplyKeyboardMarkup WithSelective(bool selective = true)
    {
        Selective = selective;
        return this;
    }
}
