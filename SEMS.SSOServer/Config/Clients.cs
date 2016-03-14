using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace SEMS.SSOServer.Config.Config
{
    public class Clients
    {
        public static List<ClientExtention> Get()
        {
            return new List<ClientExtention>
            {
                new ClientExtention
                {
                    ClientName = "SEMS-SSO",
                    ClientId = "SEMS-SSO",
                    Description = "SSO用户中心",
                    Flow = Flows.Implicit,
                    RequireConsent = false,
                    ClientUri = "http://localhost:8896/",
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
                new ClientExtention
                {
                    ClientName = "SEMS",
                    ClientId = "SEMS",
                    Description = "SEMS团队项目",
                    Flow = Flows.Implicit,
                    RequireConsent = false,
                    
                    ClientUri = "http://localhost:60544/",
                    RedirectUris = new List<string>
                                    {
                                        "http://localhost:60544/",
                                    },

                    PostLogoutRedirectUris = new List<string>
                                            {
                                                "http://localhost:60544/",
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
    /// <summary>
    /// Client的拓展类
    /// </summary>
    public class ClientExtention : Client
    {
        /// <summary>
        /// 接入SSO网站描述
        /// </summary>
        public string Description { get; set; }
    }
}