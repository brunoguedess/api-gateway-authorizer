using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIGatewayAuthorizer.Model.Auth
{
    public class PolicyDocument
    {
        [JsonPropertyName("Version")]
        public string Version { get; set; }

        [JsonPropertyName("Statement")]
        public IEnumerable<Statement> Statement { get; set; }
    }
}
