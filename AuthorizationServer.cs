using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIGatewayAuthorizer
{
    public class AuthorizationServer
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        
        public async Task<JwtSecurityToken> ValidateToken(Configuration config, string authorizationToken)
        {

            var openIdConfig = await GetOpenIdConfig(config);

            TokenValidationParameters validationParameters = GetTokenValidationParameters(config, openIdConfig);
            
            var claimsPrincipal = _tokenHandler.ValidateToken(authorizationToken, validationParameters, out var validatedToken);

            if (!HasRequiredClaims(config, claimsPrincipal))
            {
                throw new UnauthorizedAccessException("Token does not contain required claims");
            }

            var jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken;
        }

        private static TokenValidationParameters GetTokenValidationParameters(Configuration config, OpenIdConnectConfiguration openIdConfig)
        {
            return new TokenValidationParameters
            {
                ValidIssuer = config.GetAuthorizerTokenIssuer(),
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = openIdConfig.SigningKeys,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = config.GetAuthorizerAudience(),
                ValidateLifetime = true
            };
        }

        private bool HasRequiredClaims(Configuration config, ClaimsPrincipal claimsPrincipal)
        {
            return config.GetAuthorizerClaims().All(claim => claimsPrincipal.HasClaim(c =>
                    c.Type == claim.type && 
                    c.Value == claim.value
                )
            );
        }

        private async Task<OpenIdConnectConfiguration> GetOpenIdConfig(Configuration config)
        {
            var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(config.GetAuthorizerOpenIdConfig(), new OpenIdConnectConfigurationRetriever());
            var openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);

            return openIdConfig;
        }
    }

}
