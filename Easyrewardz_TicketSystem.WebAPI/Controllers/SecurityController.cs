﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Provider;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class SecurityController : ControllerBase
    {
        private IConfiguration configuration;
        private readonly string _connectioSting;

        public SecurityController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
        }
        [HttpGet]
        public string authenticate()
        {
            Result resp = new Result();
            try
            {
                securityCaller _newSecurityCaller = new securityCaller();
                string Programcode = HttpContext.Request.Headers["X_Authorized_Programcode"];
                string Domainname = HttpContext.Request.Headers["X_Authorized_Domainname"];
                string applicationid = HttpContext.Request.Headers["X_Authorized_applicationid"];
                string userId = HttpContext.Request.Headers["X_Authorized_userId"];
                string password = HttpContext.Request.Headers["X_Authorized_password"];

                if (!string.IsNullOrEmpty(Programcode) && !string.IsNullOrEmpty(Domainname) && !string.IsNullOrEmpty(applicationid) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
                {
                    string token = _newSecurityCaller.generateToken(new SecurityService(_connectioSting), Programcode, applicationid, Domainname, userId, password);
                    resp.IsResponse = true;
                    resp.statusCode = 200;
                    resp.Message = token;
                    resp.ErrorMessage = null;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(resp);
                }
                else
                {
                    resp.IsResponse = false;
                    resp.statusCode = 500;
                    resp.Message = "Request Error";
                    resp.ErrorMessage = resp.ErrorMessage;
                    return JsonConvert.SerializeObject(resp);
                }
            }
            catch (Exception _ex)
            {
                resp.IsResponse = false;
                resp.statusCode = 500;
                resp.Message = "Request Error";
                resp.ErrorMessage = _ex.InnerException.ToString();
                return JsonConvert.SerializeObject(resp);
            }
        }
    }
    public class Result
    {
        public int statusCode { get; set; }
        public string Message { get; set; }
        public bool IsResponse { get; set; }
        public string ErrorMessage { get; set; }
        public string customerFullName { get; set; }
        public string emailid { get; set; }
        public string alternateemail { get; set; }
        public string MobileNumber { get; set; }
        public int Gender { get; set; }
        public string AlternateNumber { get; set; }
    }

}