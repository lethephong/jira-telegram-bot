using System.Collections.Generic;
using Newtonsoft.Json;

namespace JiraMessage
{
    public class Changelog
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("items")]
        public IEnumerable<Item> Items { get; set; }
    }
}