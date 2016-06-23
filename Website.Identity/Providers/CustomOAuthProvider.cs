﻿using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Website.Foundation.Core.Aggregates;
using Website.Identity.Managers;
using Website.Identity.Aggregates;
using Website.Identity.Providers;
using Website.Identity.Repositories;
using Website.Identity.Helpers;
using Website.Identity.Constant;

namespace Website.Identity.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError(ErrorType.INVALID_CLIENT_ID, "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            using (AuthRepository _repo = new AuthRepository())
            {
                client = _repo.FindClient(context.ClientId);
            }

            if (client == null)
            {
                context.SetError(ErrorKey.INVALID_CLIENT_ID, string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError(ErrorKey.INVALID_CLIENT_ID, "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != HashGenerator.GetHash(clientSecret))
                    {
                        context.SetError(ErrorKey.INVALID_CLIENT_ID, "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError(ErrorKey.INVALID_CLIENT_ID, "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            allowedOrigin = allowedOrigin == null ? "*" : allowedOrigin;
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError(ErrorKey.INVALID_GRANT, "The user name or password is incorrect.");
                return;
            }

            if (!user.EmailConfirmed)
            {
                context.SetError(ErrorKey.INVALID_GRANT, "User did not confirm email.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await this.GetClaimIdentity(user, userManager);
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    { 
                        "userName", context.UserName
                    }
                });

            var ticket = new AuthenticationTicket(oAuthIdentity, props);
            context.Validated(ticket);
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError(ErrorKey.INVALID_CLIENT_ID, "Refresh token is issued to a different clientId.");
                return;
            }

            // Change auth ticket for refresh token requests
            ClaimsIdentity currentIdentity = new ClaimsIdentity(context.Ticket.Identity);
            ApplicationUserManager userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            // Get up-to-date user with claims and roles for creating new ticket
            ApplicationUser user = await userManager.FindByNameAsync(currentIdentity.Name);
            ClaimsIdentity oAuthIdentity = await this.GetClaimIdentity(user, userManager);

            var newTicket = new AuthenticationTicket(oAuthIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }


        private async Task<ClaimsIdentity> GetClaimIdentity(ApplicationUser user, ApplicationUserManager userManager)
        {
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");
            oAuthIdentity.AddClaims(ExtendedClaimsProvider.GetClaims(user));
            oAuthIdentity.AddClaims(RolesFromClaims.CreateRolesBasedOnClaims(oAuthIdentity));
            return oAuthIdentity;
        }
    }
}