using System;
using Newtonsoft.Json;

namespace JiraMessage
{
    public class Comment
    {
        [JsonProperty("author")]
        public User Author { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("self")]
        public Uri CommentUri { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("updateAuthor")]
        public User UpdateAuthor { get; set; }

        [JsonProperty("updated")]
        public DateTimeOffset Updated { get; set; }
    }
}