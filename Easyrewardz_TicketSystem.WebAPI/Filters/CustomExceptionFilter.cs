﻿using Microsoft.AspNetCore.Http;
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
            //HttpStatusCode status = HttpStatusCode.InternalServerError;
            //String message = String.Empty;

            var controllerName = (string)context.RouteData.Values["controller"];
            var actionName = (string)context.RouteData.Values["action"];

            
            string _token = Convert.ToString(context.HttpContext.Request.Headers["X-Authorized-Token"]);
            Authenticate authenticate = new Authenticate();
            authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(_token));

            var exceptionType = context.Exception.GetType();
            
            context.ExceptionHandled = true;
            //HttpResponse response = context.HttpContext.Response;
            //response.StatusCode = (int)status;
            //response.ContentType = "application/json";
            //var err = message + " " + context.Exception.StackTrace;
            //response.WriteAsync(err);
            ErrorLogCaller errorLogCaller = new ErrorLogCaller();
            ErrorLog errorLogs = new ErrorLog
            {
                ActionName = actionName,
                ControllerName = controllerName,
                TenantID = authenticate.TenantId
            };
            errorLogs.UserID = authenticate.UserMasterID;
            errorLogs.Exceptions= context.Exception.StackTrace;
            int Result = errorLogCaller.AddErrorLog(new ErrorLogging(_connectioSting), errorLogs);

        }
    }
}
