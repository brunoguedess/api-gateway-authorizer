

namespace APIGatewayAuthorizer.Error
{
    public class AuthorizationServerMessages
    {
        public static void SetContextErrorMessage(IDictionary<string, object> context, string exceptionMessage)
        {
            var errorMessage = GetErrorMessage(exceptionMessage);

            context.Add("code", errorMessage.Code);
            context.Add("message", errorMessage.Message);
        }

        private static ErrorMessage GetErrorMessage(string exceptionMessage)
        {
            var defaultErrorMessage = new ErrorMessage("\"STS00001\"", "\"Acesso não autorizado\"");

            if (exceptionMessage.StartsWith("IDX10223"))
            {
                return new ErrorMessage("\"STS00002\"", "\"Token expirado!\"");
            }

           return defaultErrorMessage;
        }
    }
}
