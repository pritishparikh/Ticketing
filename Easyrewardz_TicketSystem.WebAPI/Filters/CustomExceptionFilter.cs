using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;

namespace Easyrewardz_TicketSystem.WebAPI.Filters
{
    //[Route("api/[controller]")]
    // [ApiController]
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class CustomExceptionFilter : IExceptionFilter
    {
        #region variable declaration
        private IConfiguration configuration;
        private readonly string _connectioSting;
        private readonly string _radisCacheServerAddress;
        #endregion
        public CustomExceptionFilter(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
        }
        public void OnException(ExceptionContext context)
        {
            var controllerName = (string)context.RouteData.Values["controller"];
            var actionName = (string)context.RouteData.Values["action"];       
            string _token = Convert.ToString(context.HttpContext.Request.Headers["X-Authorized-Token"]);
            Authenticate authenticate = new Authenticate();
            authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
            var exceptionType = context.Exception.GetType();        
            context.ExceptionHandled = true;
            ErrorLogCaller errorLogCaller = new ErrorLogCaller();
            ErrorLog errorLogs = new ErrorLog
            {
                ActionName = actionName,
                ControllerName = controllerName,
                TenantID = authenticate.TenantId,
                UserID = authenticate.UserMasterID,
                Exceptions = context.Exception.StackTrace,
                MessageException = context.Exception.Message,
                IPAddress = Convert.ToString(context.HttpContext.Connection.RemoteIpAddress)
        };
            int Result = errorLogCaller.AddErrorLog(new ErrorLogging(_connectioSting), errorLogs);
            context.Result = new RedirectToRouteResult(
             new RouteValueDictionary
             {
                    { "controller", "ErrorLog" },
                    { "action", "ReturnException" }
             });
    }
    }
}
