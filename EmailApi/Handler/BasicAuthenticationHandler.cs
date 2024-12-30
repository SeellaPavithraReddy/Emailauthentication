using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EmailApi.Models.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using ISystemClock = Microsoft.AspNetCore.Authentication.ISystemClock;

namespace EmailApi.Handler
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly EmailDbContext emailDbContext;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> option, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, EmailDbContext emailDbContext) : base(option, logger, encoder, clock)
        {
            emailDbContext = emailDbContext;
        }
        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("No Header Found");


            var _headerValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(_headerValue.Parameter);
            string credentials = Encoding.UTF8.GetString(bytes);
            if (!string.IsNullOrEmpty(credentials))
            {
                string[] array = credentials.Split(":");
                string username = array[0];
                string password = array[1];
                var user = this.emailDbContext.authenticates.FirstOrDefault(item => item.UserName == username && item.Password == password);
                if (user == null)
                    return AuthenticateResult.Fail("UnAuthorized");
                var claim = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claim, Scheme.Name);
                var principle = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principle, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("UnAuthorized");

            }
        }
    }



}