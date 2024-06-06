using System.Text.Json;

namespace APIGatewayAuthorizer
{
    public interface IParameterSource
    {
        public Task<T> GetParameters<T>(string parameterName, JsonSerializerOptions serializerOptions);
    }
}
