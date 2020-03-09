using System;
using System.Text;

namespace BotMessage
{
    public abstract class BotMessageBuilder
    {
        protected readonly IBotMessageFormatter MessageFormatter;
        private readonly StringBuilder _sb;

        protected BotMessageBuilder(IBotMessageFormatter messageFormatter)
        {
            _sb = new StringBuilder();
            MessageFormatter = messageFormatter;
        }


        protected void StartBold()
        {
            _sb.Append(MessageFormatter.BoldStart);
        }

        protected void EndBold()
        {
            _sb.Append(MessageFormatter.BoldEnd);
        }

        protected void StartItalic()
        {
            _sb.Append(MessageFormatter.ItalicStart);
        }

        protected void EndItalic()
        {
            _sb.Append(MessageFormatter.ItalicEnd);
        }

        protected void StartBlockCode()
        {
            _sb.Append(MessageFormatter.BlockCodeStart);
        }

        protected void StopBlockCode()
        {
            _sb.Append(MessageFormatter.BlockCodeEnd);
        }

        protected void AppendLink(Uri target, string linkName)
        {
            AppendLink(target.ToString(), linkName);
        }

        protected void AppendLink(string target, string linkName)
        {
            _sb.Append(MessageFormatter.FormatLink(target, linkName));
        }

        protected void Append(string value, bool escape = false)
        {
            value = value ?? "";
            value = escape ? MessageFormatter.Escape(value) : value;
            _sb.Append(value);
        }

        protected void AppendBold(string value, bool escape = false)
        {
            StartBold();
            Append(value, escape);
            EndBold();
        }

        protected void AppendItalic(string value, bool escape = false)
        {
            StartItalic();
            Append(value, escape);
            EndItalic();
        }


        protected void NewLine()
        {
            _sb.Append(MessageFormatter.LineSeperator);
        }

        protected string Build()
        {
            return _sb.ToString();
        }
    }
}