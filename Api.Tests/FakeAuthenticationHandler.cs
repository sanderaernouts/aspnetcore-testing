using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Tests
{
    public static class FakeAuthentication
    {
        public const string Scheme = "Fake authentication";
    }
    public class FakeAuthenticationHandler : AuthenticationHandler<FakeAuthenticationOptions>
    {
        public FakeAuthenticationHandler(IOptionsMonitor<FakeAuthenticationOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authenticationTicket = new AuthenticationTicket(
                new ClaimsPrincipal(Options.Identity),
                new AuthenticationProperties(),
                FakeAuthentication.Scheme);

            return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }
    }

    public static class FakeAuthenticationExtensions
    {
        public static AuthenticationBuilder AddTestAuth(this AuthenticationBuilder builder, Action<FakeAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<FakeAuthenticationOptions, FakeAuthenticationHandler>(FakeAuthentication.Scheme, FakeAuthentication.Scheme, configureOptions);
        }
    }

    public class FakeAuthenticationOptions : AuthenticationSchemeOptions
    {
        public virtual ClaimsIdentity Identity { get; } = new ClaimsIdentity(new[]
        {
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", Guid.NewGuid().ToString()),
        }, "test");
    }
}
