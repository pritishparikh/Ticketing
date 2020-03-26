using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache Cache;
        public TicketDBContext Db { get; set; }
        #endregion
        public CustomExceptionFilter(IConfiguration _iConfig, IDistributedCache cache, TicketDBContext db)
        {
            configuration = _iConfig;
            Db = db;
            Cache = cache;
        }
        public void OnException(ExceptionContext context)
        {
            var controllerName = (string)context.RouteData.Values["controller"];
            var actionName = (string)context.RouteData.Values["action"];       
            string token = Convert.ToString(context.HttpContext.Request.Headers["X-Authorized-Token"]);
            Authenticate authenticate = new Authenticate();
            authenticate = SecurityService.GetAuthenticateDataFromTokenCache(Cache, SecurityService.DecryptStringAES(token));
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
            int Result = errorLogCaller.AddErrorLog(new ErrorLogging(Cache, Db), errorLogs);
            context.Result = new RedirectToRouteResult(
             new RouteValueDictionary
             {
                    { "controller", "ErrorLog" },
                    { "action", "ReturnException" }
             });
    }
    }
}
