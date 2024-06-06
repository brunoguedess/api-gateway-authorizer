using System.Text.Json.Serialization;

namespace APIGatewayAuthorizer {
    public class Claim {
        [JsonPropertyName("type")]
        public string type { get; set; }
        [JsonPropertyName("value")]
        public string value { get; set; }
        
    }
}