using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramJiraBot
{
    public class Project
    {
        public string ProjectKey;
        public ChatId ChatId;
    }
    public class JiraBotRepository : IJiraBotRepository
    {
        IMemoryCache cache;
        string filePath = @"./db/project.json";
        List<Project> projects;
        public JiraBotRepository()
        {
            cache = new MemoryCache(new MemoryCacheOptions());
            try
            {
                var text = System.IO.File.ReadAllText(filePath);
                projects = JsonConvert.DeserializeObject<List<Project>>(text);
                foreach (var project in projects)
                    cache.Set(project.ProjectKey, project.ChatId);
            }
            catch(Exception ex)
            {
                projects = new List<Project>();
            }            
        }
        public ChatId GetChatIdByProjectKey(string projectKey)
        {
            return (ChatId)cache.Get(projectKey);
        }
        public void SetChatIdToProjectKey(ChatId chatId, string projectKey)
        {
            cache.Set(projectKey, chatId);
            projects.Remove(projects.Where(x => x.ProjectKey == projectKey).FirstOrDefault());
            projects.Add(new Project { ChatId = chatId, ProjectKey = projectKey });
            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(projects));
        }        
    }
}
