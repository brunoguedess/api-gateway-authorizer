using APIGatewayAuthorizer.Model;
using APIGatewayAuthorizer.Model.Auth;

namespace APIGatewayAuthorizer
{
    public class AuthPolicyBuilderCustom : AuthPolicyBuilder
    {
        private readonly HttpVerb _verb;
        private readonly string _resource;

        public AuthPolicyBuilderCustom(ApiGatewayArn methodArn, string principalId) : 
            base(principalId, methodArn.AwsAccountId, new ApiOptions(methodArn.Region, methodArn.RestApiId, methodArn.Stage))
        {
            _verb = HttpVerb.Verb(methodArn.Verb);
            _resource = "/" + methodArn.Resource;
        }


        public AuthPolicy BuildAuthorizedPolicy()
        {
            AllowMethod(_verb, _resource);

            return Build();
        }

        public AuthPolicy BuildUnauthorizedPolicy()
        {
            DenyMethod(_verb, _resource);

            return Build();
        }
    }
}
