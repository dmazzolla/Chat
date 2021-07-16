using Newtonsoft.Json;
using System;

namespace Chat.Model
{
    public class QueueModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("state")]
        public string State { get; set; }
        
        [JsonProperty("idle_since")]
        public DateTime IdleSince { get; set; }
        
    }
}
