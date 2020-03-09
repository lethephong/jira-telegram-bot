using System;
using Newtonsoft.Json;

namespace JiraMessage
{
    public class Attachment
    {
        [JsonProperty("author")]
        public User Author { get; set; }

        [JsonProperty("content")]
        public Uri Content { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        [JsonProperty("self")]
        public Uri PriorityUri { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("thumbnail")]
        public Uri Thumbnail { get; set; }
    }
}