﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
namespace Authe
{
    public class MyAuth:OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        //
        // Summary:
        //     Called when a request to the Token endpoint arrives with a "grant_type" of "password".
        //     This occurs when the user has provided name and password credentials directly
        //     into the client application's user interface, and the client application is using
        //     those to acquire an "access_token" and optional "refresh_token". If the web application
        //     supports the resource owner credentials grant type it must validate the context.Username
        //     and context.Password as appropriate. To issue an access token the context.Validated
        //     must be called with a new ticket containing the claims about the resource owner
        //     which should be associated with the access token. The application should take
        //     appropriate measures to ensure that the endpoint isn’t abused by malicious callers.
        //     The default behavior is to reject this grant type. See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        //
        // Parameters:
        //   context:
        //     The context of the event carries information in and results out.
        //
        // Returns:
        //     Task to enable asynchronous execution
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if(context.UserName=="admin" && context.Password=="password")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("UserName", "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Chaitanya"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid user");
            }
        }
        
    }
}