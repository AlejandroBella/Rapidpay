// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
             new List<ApiScope>
        {
            new ApiScope("RapidPay.Card", "Card APi")
        };

        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
               new Client
               {
                    ClientId = "RapidPay.Client",
                    ClientSecrets = new [] { new Secret("rapidSecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "RapidPay.Card" }
                }
        };
    }
}