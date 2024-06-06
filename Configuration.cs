using System.Text.Json;
using System.Text.Json.Serialization;
using APIGatewayAuthorizer;
using APIGatewayAuthorizer.Model;

namespace APIGatewayAuthorizer
{

    public class Configuration
    {
        private readonly IParameterSource _parameterSource;
        private readonly ApiGatewayArn _methodArn;
        private readonly JsonSerializerOptions _serializerOptions;
        private ApiGatewayAuthorizer _apiGatewayAuthorizer;

        public Configuration(IParameterSource parameterSource, ApiGatewayArn methodArn)
        {
            this._parameterSource = parameterSource;
            this._methodArn = methodArn;

            this._serializerOptions = new JsonSerializerOptions
            {
                TypeInfoResolver = JsonSerializerApiGatewayAuthorizer.Default
            };
        }

        public async Task<Configuration> Load()
        {
            var apiGatewayAuthorizerParametersName = $"/api-gateway/account/{_methodArn.AwsAccountId}/api/{_methodArn.RestApiId}";

            this._apiGatewayAuthorizer = await _parameterSource.GetParameters<ApiGatewayAuthorizer>(apiGatewayAuthorizerParametersName, _serializerOptions);

            return this;
        }

        public string GetAuthorizerApplication()
        {
            return _apiGatewayAuthorizer.application;
        }

        public string GetAuthorizerOpenIdConfig()
        {
            return _apiGatewayAuthorizer.openIdConfig;
        }

        public string GetAuthorizerTokenIssuer()
        {
            return _apiGatewayAuthorizer.tokenIssuer;
        }

        public string GetAuthorizerAudience()
        {
            return _apiGatewayAuthorizer.audience;
        }

        public List<Claim> GetAuthorizerClaims()
        {
            return _apiGatewayAuthorizer.claims;
        }
    }
}

[JsonSerializable(typeof(ApiGatewayAuthorizer))]
[JsonSerializable(typeof(Claim))]
public partial class JsonSerializerApiGatewayAuthorizer : JsonSerializerContext
{
    // By using this partial class derived from JsonSerializerContext, we can generate reflection free JSON Serializer code at compile time
    // which can deserialize our class and properties. However, we must attribute this class to tell it what types to generate serialization code for.
    // See https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-source-generation
}