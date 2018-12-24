using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace YTCute.Identity.API.Configuration
{
    public class Config
    {
        // ApiResources define the apis in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Mvc", "MVC Client"),
            };
        }

        // Identity resources are data like user ID, name, or email address of a user
        // see: http://docs.identityserver.io/en/release/configuration/resources.html
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        // client want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {

            return new List<Client>
            {
                new Client
        {
            ClientId = "Mvc",
            ClientName = "MVC Client",
            ClientSecrets = new List<Secret>
             {
                 new Secret("secret".Sha256())
             },
            AllowedGrantTypes =  GrantTypes.Hybrid,
            AllowAccessTokensViaBrowser = false,
            RequireConsent = false,
            AllowOfflineAccess = true,
            AlwaysIncludeUserClaimsInIdToken = true,
            RedirectUris = { "http://localhost:5002/signin-oidc" },//登录成功后返回的客户端地址
            PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },//注销登录后返回的客户端地址
            AllowedScopes =//下面这两个必须要加吧 不太明白啥意思
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                "Mvc"
            }
        },
                 // resource owner password grant client
        new Client
        {
            ClientId = "ro.client",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedScopes = { "Mvc" }
        }

            };
        }


        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>{
        new TestUser
        {
            SubjectId = "1",
            Username = "alice",
            Password = "password"
        },
        new TestUser
        {
            SubjectId = "2",
            Username = "bob",
            Password = "password"
        }
        ,
        new TestUser
        {
            SubjectId = "3",
            Username = "user3",
            Password = "password"
        }
    };
        }
    }
}