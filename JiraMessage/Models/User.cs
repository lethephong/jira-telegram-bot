using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JiraMessage
{
    public class User
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("avatarUrls")]
        public Dictionary<string, Uri> AvatarUris { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("emailAddress")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("self")]
        public Uri UserUri { get; set; }
    }
}