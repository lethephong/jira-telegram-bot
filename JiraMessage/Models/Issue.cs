using System;
using Newtonsoft.Json;

namespace JiraMessage
{
    public class Issue
    {
        [JsonProperty("fields")]
        //[JsonConverter(typeof(IssueFieldJsonConverter))]
        public Fields Fields { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("self")]
        public Uri IssueUri { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}