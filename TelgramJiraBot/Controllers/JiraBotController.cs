using System;
using System.Text.Json;
using System.Threading.Tasks;
using JiraMessage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace TelegramJiraBot
{
    [Route("api/[controller]")]
    public class JiraBotController : Controller
    {
        private readonly IJiraBotService _jiraBot;
        private ILogger<JiraBotController> _logger;
        private readonly string _jiraToken;

        public JiraBotController(IOptions<JiraBotConfig> config, IJiraBotService jiraBotService, ILogger<JiraBotController> logger)
        {
            _jiraBot = jiraBotService;
            _logger = logger;
            _jiraToken = config.Value.WebToken;
        }

        public IActionResult Index()
        {            
            return Ok("JiraBot");
        }
        [HttpPost]
        [Route("[action]/{token}/{projectKey}/{issueKey}")]
        public async Task<IActionResult> Webhook(string token, string projectKey, string issueKey, [FromBody] dynamic payload)
        {
            if (token != _jiraToken)
                return NotFound();            
            //_logger.LogInformation("projectKey:" + projectKey + ", issueKey:" + issueKey);
            var updateStr = ((JsonElement)payload).ToString();
            //_logger.LogInformation(updateStr);                       
            try
            {                
                var update = JsonConvert.DeserializeObject<Update>(updateStr);           
                await _jiraBot.ProcessNotification(update, projectKey, issueKey);
            }
            catch(Exception ex)
            {
                _logger.LogError("projectKey:" + projectKey + ", issueKey:" + issueKey);
                _logger.LogError(updateStr);
                _logger.LogError(ex, ex.Message);
            }
            return Ok();
        }
    }
}