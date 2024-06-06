namespace APIGatewayAuthorizer.Error
{
    internal class UnauthorizedException : System.Exception
    {
        public UnauthorizedException() : base("Unauthorized")
        {
        }
    }
}
