using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BotMessage;
using Microsoft.Extensions.Options;
using NLog;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramJiraBot
{
    public class BotCommand
    {
        public Message Message;
        public string Command;
        public string[] Parameters;
    }
    public class JiraBotService : IJiraBotService
    {
        Logger logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
        private ITelegramBotClient botClient;
        private readonly JiraBotConfig config;
        IJiraBotRepository repo;
        private User bot;

        public JiraBotService(JiraBotConfig jiraConfig)
        {            
            config = jiraConfig;
            Start();            
        }
        private void Start()
        {
            botClient = new TelegramBotClient(config.Token);
            repo = new JiraBotRepository();
            botClient.OnMessage += BotOnMessage;
            botClient.StartReceiving();
            bot = botClient.GetMeAsync().Result;
        }
        public async Task<Message> ProcessNotification(JiraMessage.Update update, string projectKey,
            string issueKey)
        {            
            var formatter = new HtmlBotMessageFormatter();
            var message = new JiraMessage.JiraMessageBuilder(update, projectKey, issueKey, new HtmlBotMessageFormatter());
            var chatId = repo.GetChatIdByProjectKey(projectKey);
            return await botClient.SendTextMessageAsync(chatId, message.Build(), formatter.ParseMode,
                disableWebPagePreview: true);            
        }
        private async void BotOnMessage(object sender, MessageEventArgs e)
        {           
            try
            {
                if (e.Message.Text != null && (e.Message.Chat.Type == ChatType.Private || e.Message.Text.Contains(bot.Username)))
                {
                    var botCommand = ProcessMessage(e.Message);
                    await ProcessCommand(botCommand);
                }
            }
            catch(Exception ex)
            {
                logger.Error(e.Message.Text);
                logger.Error(ex);
            }            
        }
        private BotCommand ProcessMessage(Message message)
        {
            // First remove username
            var text = message.Text.Replace("@"+bot.Username, string.Empty);
            var parameters = text.Split(" ", System.StringSplitOptions.RemoveEmptyEntries);
            return new BotCommand { 
                Message = message,
                Command = parameters[0],
                Parameters = parameters[1..]
            };
        }
        
        private async Task ProcessCommand(BotCommand botCommand)
        {
            if (botCommand.Command == "/start")
                await ProcessStartCommand(botCommand);
            if (botCommand.Command == "/map")
                await ProcessMapCommand(botCommand);
        }
        private async Task ProcessMapCommand(BotCommand botCommand)
        {
            var text = "";
            var projectKey = (botCommand.Parameters != null && botCommand.Parameters.Length > 0) ? botCommand.Parameters[0] : "";
            if (projectKey == string.Empty || projectKey == "")
                text = "Invalid parameter. Using command /map@botUsername projectKey in group to map with jira project.";
            else
            {
                repo.SetChatIdToProjectKey(botCommand.Message.Chat.Id, projectKey);
                text = "Map this chat group to Jira Project with projectKey:" +
                    botCommand.Parameters[0] + " successful.";
            }                
            await botClient.SendTextMessageAsync(botCommand.Message.Chat.Id, text, ParseMode.Html, disableWebPagePreview: true);
        }

        private async Task ProcessStartCommand(BotCommand botCommand)
        { 
            var text =
                "Hey, add the following Url to your Jira webhooks see the <a href=\"https://developer.atlassian.com/jiradev/jira-apis/webhooks#Webhooks-Registeringawebhook\">Jira Documentation</a> fore more information\n<b>Webhook Url:</b>\n" +
                config.JiraWebhookUrl + "/" +
                config.WebToken +
                "/${project.key}/${issue.key}";
            text += "\n" + "Using command /map@botUsername projectKey in group chat to map project group chat"; 
            await botClient.SendTextMessageAsync(botCommand.Message.Chat.Id, text, ParseMode.Html, disableWebPagePreview: true);
        }
    }
}