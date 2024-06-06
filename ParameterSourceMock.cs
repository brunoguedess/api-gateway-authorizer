namespace APIGatewayAuthorizer
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
  
    public class ParameterSourceMock : IParameterSource
    {
        /*
        private readonly IDictionary<string, string> _parameters = new Dictionary<string, string>()
        {
            {"/api-gateway/account/654654184760/config/openid_config", "https://login.microsoftonline.com/43f171fd-2abf-44b0-a3f4-0198e2ddf189/v2.0/.well-known/openid-configuration"},
            {"/api-gateway/account/654654184760/config/token_issuer", "https://sts.windows.net/43f171fd-2abf-44b0-a3f4-0198e2ddf189/"},
            {"/api-gateway/account/654654184760/api/87e4t1h68a", "api://api-externa-1"},
        };
        */

        private readonly IDictionary<string, string> _parameters = new Dictionary<string, string>()
        {
            {"/api-gateway/account/654654184760/api/1p0iqoqc8b", "{\"application\": \"CCL\", \"openIdConfig\": \"https://login.microsoftonline.com/43f171fd-2abf-44b0-a3f4-0198e2ddf189/v2.0/.well-known/openid-configuration\", \"tokenIssuer\": \"https://sts.windows.net/43f171fd-2abf-44b0-a3f4-0198e2ddf189/\", \"audience\": \"api://api-externa-1\", \"claims\": [{\"type\": \"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\", \"value\": \"write:api-externa-1\"}, {\"type\": \"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\",\"value\": \"read:api-externa-1\"}]}"}
        };

        public async Task<T> GetParameters<T>(string parametersName, JsonSerializerOptions serializerOptions)
        {
            parametersName = "/api-gateway/account/654654184760/api/1p0iqoqc8b";

            string jsonString = _parameters[parametersName];

            T parameters = JsonSerializer.Deserialize<T>(jsonString);

            return parameters;
        }
    }
}
