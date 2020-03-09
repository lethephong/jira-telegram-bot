using System;
using Telegram.Bot.Types.Enums;

namespace BotMessage
{
    public class HtmlBotMessageFormatter : IBotMessageFormatter
    {
        public string Escape(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (message == string.Empty) return message;
            return message.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
        }

        /// <summary>
        ///     Creates a link.
        /// </summary>
        /// <param name="linkTarget">the target url of the link.</param>
        /// <param name="linkText">the text of the link. Will always be escaped. </param>
        /// <returns></returns>
        public string FormatLink(string linkTarget, string linkText)
        {
            return $"<a href=\"{linkTarget}\">{Escape(linkText)}</a>";
        }

        public string FormatBold(string message)
        {
            return $"{BoldStart}{message}{BoldEnd}";
        }

        public string FormatItalic(string message)
        {
            return $"{ItalicStart}{message}{ItalicEnd}";
        }

        public string FormatInlineCode(string message)
        {
            return $"<code>{message}</code>";
        }

        public string FormatBlockCode(string message)
        {
            return $"{BlockCodeStart}{message}{BlockCodeEnd}";
        }

        public string FormatMention(int userId, string linkText)
        {
            return FormatLink($"tg://user?id={userId}", linkText);
        }

        public string BoldStart => "<b>";


        public string BoldEnd => "</b>";


        public string ItalicStart => "<i>";


        public string ItalicEnd => "</i>";


        public string BlockCodeStart => "<code>";


        public string BlockCodeEnd => "</code>";
        public string LineSeperator => "\n";
        public ParseMode ParseMode => ParseMode.Html;
    }
}