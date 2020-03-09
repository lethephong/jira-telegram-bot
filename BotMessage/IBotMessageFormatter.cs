using Telegram.Bot.Types.Enums;

namespace BotMessage
{
    public interface IBotMessageFormatter
    {
        string BlockCodeEnd { get; }
        string BlockCodeStart { get; }
        string BoldEnd { get; }

        string BoldStart { get; }
        string ItalicEnd { get; }
        string ItalicStart { get; }
        string LineSeperator { get; }
        ParseMode ParseMode { get; }
        string Escape(string message);
        string FormatLink(string linkTarget, string linkText);
        string FormatBold(string message);
        string FormatItalic(string message);
        string FormatInlineCode(string message);
        string FormatBlockCode(string message);
        string FormatMention(int userId, string linkText);
    }
}