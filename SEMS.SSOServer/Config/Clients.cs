using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace SEMS.SSOServer.Config.Config
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "SEMS",
                    ClientId = "SEMS",
                    Flow = Flows.Implicit,
                    RequireConsent = false,
                    RedirectUris = new List<string>
                                    {
                                        "http://localhost:8896/",
                                    },

                    PostLogoutRedirectUris = new List<string>
                                            {
                                                "http://localhost:8896/",
                                            },

                    AllowedScopes = new List<string>
                                    {
                                        "openid",
                                        "profile",
                                        "roles",
                                        "sampleApi"
                                    },
                },
            };
        }
    }
}