using System.Text.Json.Serialization;

namespace APIGatewayAuthorizer {
    public class ApiGatewayAuthorizer {
        [JsonPropertyName("application")]
        public string application { get; set; }
        [JsonPropertyName("openIdConfig")]
        public string openIdConfig { get; set; }
        [JsonPropertyName("tokenIssuer")]
        public string tokenIssuer { get; set; }
        [JsonPropertyName("audience")]
        public string audience { get; set; }
        [JsonPropertyName("claims")]
        public List<Claim> claims { get; set; }

        public override string ToString()
        {
            return $"Application: {application}, OpenIdConfig: {openIdConfig}, TokenIssuer: {tokenIssuer}, Audience: {audience}, Claims: {string.Join(", ", claims)}";
        }
    }
}