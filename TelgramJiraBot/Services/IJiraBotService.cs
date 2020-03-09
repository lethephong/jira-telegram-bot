using JiraMessage;
using System.Threading.Tasks;

namespace TelegramJiraBot
{
    public interface IJiraBotService
    { 
        Task<Telegram.Bot.Types.Message> ProcessNotification(Update update, string projectKey, string issueKey);
    }
}