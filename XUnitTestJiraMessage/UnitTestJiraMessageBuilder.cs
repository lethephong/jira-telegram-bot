using BotMessage;
using JiraMessage;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace XUnitTestJiraMessage
{
    public class UnitTestJiraMessageBuilder
    {
        [Fact]
        public void Test_IssueCreatedOk()
        {
            var text = File.ReadAllText("./update/issue_created.json");
            var update = JsonConvert.DeserializeObject<Update>(text);
            var builder = new JiraMessageBuilder(update, "AON", "AON-25", new HtmlBotMessageFormatter());
            var message = builder.Build();            
        }
        [Fact]
        public void Test_IssueUpdate_CommentDeleted_Ok()
        {
            var text = File.ReadAllText("./update/issue_comment_deleted.json");
            var update = JsonConvert.DeserializeObject<Update>(text);
            var builder = new JiraMessageBuilder(update, "AON", "AON-25", new HtmlBotMessageFormatter());
            var message = builder.Build();
        }
    }
}
