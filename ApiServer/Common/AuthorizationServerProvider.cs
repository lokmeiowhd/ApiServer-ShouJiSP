using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace ApiServer.Common
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization,content-type,cache-control" });
                context.RequestCompleted();
                return Task.FromResult(0);
            }

            return base.MatchEndpoint(context);
        }
        /// <summary>
        /// grant_type=password
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            string a = await Task<string>.Run(() =>
             {
                 return "1";
             });
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("level", a));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "webuser"));
            context.OwinContext.Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
            context.Validated(identity);
        }
        /// <summary>
        /// 验证客户[client_id与client_secret验证]
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //http://localhost:48339/token
            //grant_type=client_credentials&client_id=irving&client_secret=123456
            //grant_type=client_credentials&client_id=web&client_secret=e46232c20609435eba54b7576de935dd
            string client_id;
            string client_secret;
            context.TryGetFormCredentials(out client_id, out client_secret);
            //web端接口标识
            if (client_id == "web" && client_secret == "e46232c20609435eba54b7576de935dd")
            {
                //<Guid("E46232C2-0609-435E-BA54-B7576DE935DD")>
                context.Validated(client_id);
            }
            //移动端接口标识
            else if (client_id == "mobile" && client_secret == "47a4259d267a404f91751cc2ee785a7a")
            {
                context.Validated(client_id);
            }
            else
            {
                context.SetError("invalid_client", "client is not valid");
            }
            await base.ValidateClientAuthentication(context);
        }

        /// <summary>
        /// 客户端授权[生成access token]
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, "iphone"));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties() { AllowRefresh = true });
            context.Validated(ticket);
            return base.GrantClientCredentials(context);
        }

        /// <summary>
        /// 刷新Token[刷新refresh_token]
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            //enforce client binding of refresh token
            if (context.Ticket == null || context.Ticket.Identity == null || !context.Ticket.Identity.IsAuthenticated)
            {
                context.SetError("invalid_grant", "Refresh token is not valid");
            }
            else
            {
                //Additional claim is needed to separate access token updating from authentication 
                //requests in RefreshTokenProvider.CreateAsync() method
            }
            return base.GrantRefreshToken(context);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == "irving")
            {
                var expectedRootUri = new Uri(context.Request.Uri, "/");
                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }
            return Task.FromResult<object>(null);
        }
    }
}