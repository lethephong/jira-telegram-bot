using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramJiraBot
{   
    public interface IJiraBotRepository
    {
        ChatId GetChatIdByProjectKey(string projectKey);
        void SetChatIdToProjectKey(ChatId chatId, string projectKey);
    }    
}
