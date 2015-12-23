using Owin;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using SEMS.SSOServer.Config.Config;
using SEMS.SSOServer.Service;
using SEMS.SSOServer.Config;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using System.Security.Claims;
using IdentityServer3.Core;
using Microsoft.Owin.Security;
using IdentityModel.Client;
using Microsoft.Owin.Security.Cookies;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.IdentityModel.Tokens;
using System.Collections.Generic;

namespace SEMS.SSOServer
{
    internal class Startup
    {
        private readonly string _ssoUrl = "http://localhost:8896/";
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.Map("/identity", coreApp =>
            {
                var factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get());

                factory.ViewService = new Registration<IViewService>(typeof(ViewService));
                factory.UserService = new Registration<IUserService, UserService>();
              

                var options = new IdentityServerOptions
                {
                    SiteName = "SEMS-SSO",
                    RequireSsl = false,
                    SigningCertificate = Certificate.Get(),
                    Factory = factory,
                    AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
                    {
                        EnablePostSignOutAutoRedirect = true,
                        EnableSignOutPrompt = false,
                        InvalidSignInRedirectUrl = _ssoUrl
                    },
                };

                coreApp.UseIdentityServer(options);
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = OpenIdConnectAuthenticationDefaults.AuthenticationType,
            });


            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = _ssoUrl.TrimEnd('/') + "/identity",

                ClientId = "SEMS-SSO",
                Scope = "openid profile",
                ResponseType = "id_token token",
                RedirectUri = _ssoUrl,
                PostLogoutRedirectUri = _ssoUrl,
                SignInAsAuthenticationType = OpenIdConnectAuthenticationDefaults.AuthenticationType,
                UseTokenLifetime = false,

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var nid = new ClaimsIdentity(n.AuthenticationTicket.Identity.AuthenticationType, Constants.ClaimTypes.Name, Constants.ClaimTypes.Role);

                        // get userinfo data
                        var userInfoClient = new UserInfoClient(
                        new Uri(n.Options.Authority + "/connect/userinfo"),
                        n.ProtocolMessage.AccessToken);

                        var userInfo = await userInfoClient.GetAsync();
                        userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Item1, ui.Item2)));

                        // keep the id_token for logout
                        nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));
                        // add access token for sample API
                        nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));
                        // keep track of access token expiration
                        nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));
                        // add some other app specific claim
                        nid.AddClaim(new Claim("app_specific", "some data"));
                        n.AuthenticationTicket = new AuthenticationTicket(nid, n.AuthenticationTicket.Properties);
                    },

                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }

                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}