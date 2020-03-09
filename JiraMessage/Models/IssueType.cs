using System;
using Newtonsoft.Json;

namespace JiraMessage
{
    public class IssueType
    {
        [JsonProperty("avatarId")]
        public int AvatarId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subtask")]
        public bool Subtask { get; set; }

        [JsonProperty("self")]
        public Uri TypeUri { get; set; }
    }
}