using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Filters
{
    public class PermissionRequirement : AuthenticationHandler<ModuleAuthenticationOptions>
    {
        public PermissionRequirement(IOptionsMonitor<ModuleAuthenticationOptions> options,
           ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
               : base(options, logger, encoder, clock)
        {

        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.Run(() => Authenticate());
        }
        private AuthenticateResult Authenticate()
        {
            ETSContext _DBContext = new ETSContext();
            string token = Context.Request.Headers["X-Authorized-Header"];
            int ModuleID = 1;
            if (token == null || ModuleID == 0) return AuthenticateResult.Fail("No Authorization provided");
            try
            {
                
                validateSecurityToken(token, ModuleID);
                
                var claims = new[] { new Claim(ClaimTypes.Name, token) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Failed to validate token");
            }
        }
        public DataSet validateSecurityToken(string SecretToken, int ModuleID)
        {
           
            DataSet ds = validateSecurityToken(SecretToken, ModuleID);
            return ds;

        }
    }
    public static class PermissionModuleConst
    {
        public const string ModuleAuthenticationDefaultScheme = "ModuleAuthenticationScheme";

    }
    public class ModuleAuthenticationOptions : AuthenticationSchemeOptions
    {

    }
}
