using Newtonsoft.Json;

namespace JiraMessage
{
    public class Item
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("fieldtype")]
        public string FieldType { get; set; }

        [JsonProperty("to")]
        public string New { get; set; }

        [JsonProperty("toString")]
        public string NewString { get; set; }

        [JsonProperty("from")]
        public string Old { get; set; }

        [JsonProperty("fromString")]
        public string OldString { get; set; }
    }
}