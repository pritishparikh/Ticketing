using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.CustomModel;
using Microsoft.AspNetCore.Routing;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Newtonsoft.Json;

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
        private readonly string _ErconnectioSting;
        private readonly string _radisCacheServerAddress;
        private readonly string _ClientAPIUrl;
        private readonly bool _SeeErrorLog;
        #endregion
        public CustomExceptionFilter(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
            _ErconnectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessErMasterMySqlProvider");
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            _ClientAPIUrl = configuration.GetValue<string>("ClientAPIURL");
            _SeeErrorLog = configuration.GetValue<bool>("ErrorLog");
        }
        public void OnException(ExceptionContext context)
        {
            var controllerName = (string)context.RouteData.Values["controller"];
            var actionName = (string)context.RouteData.Values["action"];
            string _token = Convert.ToString(context.HttpContext.Request.Headers["X-Authorized-Token"]);
            string ClientAPIResponse = string.Empty;

            Authenticate authenticate = new Authenticate();
            if (!string.IsNullOrEmpty(_token))
            {
                authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));
            }
            else
            {
                authenticate.TenantId = 0;
                authenticate.UserMasterID = 0;
            }

            var exceptionType = context.Exception.GetType();
            context.ExceptionHandled = true;

            try
            {

                if (_SeeErrorLog)
                {
                    ErrorLogCaller errorLogCaller = new ErrorLogCaller();
                    ErrorLog errorLogs = new ErrorLog
                    {
                        ActionName = actionName,
                        ControllerName = controllerName,
                        TenantID = authenticate.TenantId,
                        UserID = authenticate.UserMasterID,
                        Exceptions = context.Exception.StackTrace,
                        MessageException = context.Exception.Message  + " : : InnerException = " + context.Exception.ToString(),
                        IPAddress = Convert.ToString(context.HttpContext.Connection.RemoteIpAddress)
                    };
                    int result = errorLogCaller.AddErrorLog(new ErrorLogging(_ErconnectioSting), errorLogs);
                }

                ElasticErrorLogModel Elastic = new ElasticErrorLogModel
                {
                    applicationId = "2",
                    actionName = actionName,
                    controllerName = controllerName,
                    tenantID = Convert.ToString(authenticate.TenantId),
                    userID = Convert.ToString(authenticate.UserMasterID),
                    exceptions = context.Exception.StackTrace,
                    messageException = context.Exception.Message + " : : InnerException = " + context.Exception.ToString(),
                    ipAddress = Convert.ToString(context.HttpContext.Connection.RemoteIpAddress)
                };

                ClientAPIResponse = CommonService.SendApiRequest(_ClientAPIUrl + "api/InsertApplicationLog", JsonConvert.SerializeObject(Elastic));

            }
            catch(Exception)
            {

            }
          
            context.Result = new RedirectToRouteResult(
             new RouteValueDictionary
             {
                    { "controller", "ErrorLog" },
                    { "action", "ReturnException" }
             });
        }
        public void OnExceptioninanyclass(ErrorLog errorLog )
        {
            try
            {
                if (_SeeErrorLog)
                {
                    ErrorLogCaller errorLogCaller = new ErrorLogCaller();
                    int result = errorLogCaller.AddErrorLog(new ErrorLogging(_ErconnectioSting), errorLog);
                }

                string ClientAPIResponse = string.Empty;
                ElasticErrorLogModel Elastic = new ElasticErrorLogModel
                {
                    applicationId = "2",
                    actionName = errorLog.ActionName,
                    controllerName = errorLog.ControllerName,
                    tenantID = Convert.ToString(errorLog.TenantID),
                    userID = Convert.ToString(errorLog.UserID),
                    exceptions = errorLog.Exceptions,
                    messageException = errorLog.MessageException,
                    ipAddress = errorLog.IPAddress
                };

                ClientAPIResponse = CommonService.SendApiRequest(_ClientAPIUrl + "api/InsertApplicationLog", JsonConvert.SerializeObject(Elastic));

            }
            catch (Exception)
            {

            }
        }
    }
}
