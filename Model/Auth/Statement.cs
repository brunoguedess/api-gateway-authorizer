using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIGatewayAuthorizer.Model.Auth
{
    public class Statement
    {
        [JsonPropertyName("Action")]
        public string Action { get; set; }
        [JsonPropertyName("Effect")]
        public string Effect { get; set; } = "Deny"; // Default to Deny to ensure Allows are explicitly set
        [JsonPropertyName("Resource")]
        public string Resource { get; set; }
        [JsonPropertyName("Condition")]
        public IDictionary<ConditionOperator, IDictionary<ConditionKey, string>> Condition { get; set; }
    }
}
