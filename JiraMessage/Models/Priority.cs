using System;
using Newtonsoft.Json;

namespace JiraMessage
{
    public class Priority
    {
        [JsonProperty("iconUrl")]
        public Uri IconUri { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("self")]
        public Uri PriorityUri { get; set; }
    }
}