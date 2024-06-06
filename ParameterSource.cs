namespace APIGatewayAuthorizer
{
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ParameterStore : IParameterSource
    {
        public async Task<T> GetParameters<T>(string parametersName, JsonSerializerOptions serializerOptions)
        {
            var port = 2773;
            var url = $"http://localhost:{port}/systemsmanager/parameters/get/?name={parametersName}";

            var AwsSessionToken = Environment.GetEnvironmentVariable("AWS_SESSION_TOKEN");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Aws-Parameters-Secrets-Token", AwsSessionToken);

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            T parameters = DeserializeParameters<T>(responseBody, serializerOptions);

            return parameters;
        }

        private T DeserializeParameters<T>(string responseBody, JsonSerializerOptions serializerOptions)
        {
            try
            {
                using (JsonDocument document = JsonDocument.Parse(responseBody))
                {
                    JsonElement root = document.RootElement;
                    JsonElement parameterJsonValue = root.GetProperty("Parameter").GetProperty("Value");

                    var parameterJsonString = parameterJsonValue.GetString();
                    
                    return JsonSerializer.Deserialize<T>(parameterJsonString, serializerOptions);
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Exception: {ex.Message}");
                Console.WriteLine($"Path: {ex.Path}");
                Console.WriteLine($"LineNumber: {ex.LineNumber}");
                Console.WriteLine($"BytePositionInLine: {ex.BytePositionInLine}");

                throw;
            }
        }
    }
}


