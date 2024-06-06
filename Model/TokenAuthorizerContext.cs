using System.Text.Json.Serialization;

namespace APIGatewayAuthorizer.Model
{
    public class TokenAuthorizerContext
    {
        [JsonPropertyName("Type")]
        public string Type { get; set; }
        [JsonPropertyName("AuthorizationToken")]
        public string AuthorizationToken { get; set; }
        [JsonPropertyName("MethodArn")]
        public string MethodArn { get; set; }
    }
}
