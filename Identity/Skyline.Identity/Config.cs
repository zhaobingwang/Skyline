using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Identity
{
    public class Config
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("skyline.sample.api","Skyline Sample API")
            };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId="client.test.consoleapp",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes=GrantTypes.ClientCredentials,

                // secret for authentication
                ClientSecrets={
                    new Secret("123456".Sha256())
                },

                // scopes that client has access to
                AllowedScopes={ "skyline.sample.api" }
            }
        };
    }
}
