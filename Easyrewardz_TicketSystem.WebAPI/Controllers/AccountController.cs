﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.MySqlDBContext;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{

    [Route("api/[controller]")]

    [ApiController]
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    //[Authorize(AuthenticationSchemes = PermissionModuleConst.ModuleAuthenticationDefaultScheme)]
    public class AccountController : ControllerBase
    {
        #region Variable
        private IConfiguration configuration;
        private readonly IDistributedCache _Cache;
        internal static TicketDBContext Db { get; set; }
        #endregion

        #region Constructor
        public AccountController(IConfiguration _iConfig,TicketDBContext db,IDistributedCache cache)
        {
            configuration = _iConfig;
            Db = db;
            _Cache = cache;
        }
        #endregion

        #region Custom Methos 

        #region Forgot password screen
        /// <summary>
        /// Forgot password screen
        /// </summary>
        /// <returns></returns>
        //Send mail for Forgot Password
        [HttpPost]
        [Route("ForgetPassword")]
        [AllowAnonymous]
        public ResponseModel ForgetPassword(string EmailId)
        {
            ResponseModel _objResponseModel = new ResponseModel();

            try
            {
                /////Validate User
                securityCaller _securityCaller = new securityCaller();
                Authenticate authenticate = _securityCaller.validateUserEmailId(new SecurityService(_Cache,Db), EmailId);
                if (authenticate.UserMasterID > 0)
                {
                    MasterCaller masterCaller = new MasterCaller();
                    SMTPDetails sMTPDetails = masterCaller.GetSMTPDetails(new MasterServices(_Cache, Db), authenticate.TenantId);

                    CommonService commonService = new CommonService();
                    string encryptedEmailId = commonService.Encrypt(EmailId);
                    string url = configuration.GetValue<string>("websiteURL") + "/userforgotPassword?Id:" + encryptedEmailId;
                   // string body = "Hello, This is Demo Mail for testing purpose. <br/>" + url;

                    string content = "";
                    string subject = "";

                    _securityCaller.GetForgetPassowrdMailContent(new SecurityService(_Cache,Db), authenticate.TenantId, url, EmailId, out content, out subject);

                    bool isUpdate = _securityCaller.sendMail(new SecurityService(_Cache,Db), sMTPDetails, EmailId, subject, content, authenticate.TenantId);

                    if (isUpdate)
                    {
                        _objResponseModel.Status = true;
                        _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                        _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                        _objResponseModel.ResponseData = "Mail sent successfully";
                    }
                    else
                    {
                        _objResponseModel.Status = false;
                        _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                        _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                        _objResponseModel.ResponseData = "Mail sent failure";
                    }
                }
                else
                {
                    _objResponseModel.Status = false;
                    _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.RecordNotFound;
                    _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.RecordNotFound);
                    _objResponseModel.ResponseData = "Sorry User does not exist or active";
                }
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                _objResponseModel.ResponseData = "We had an error! Sorry about that";
            }
            return _objResponseModel;
        }

        #endregion

        #region Update Password 
        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="cipherEmailId">Email Id in encrypted text</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePassword")]
        [AllowAnonymous]
        public ResponseModel UpdatePassword(string cipherEmailId, string Password)
        {
            ResponseModel _objResponseModel = new ResponseModel();

            try
            {
                securityCaller _newSecurityCaller = new securityCaller();

                CommonService commonService = new CommonService();
                string encryptedEmailId = commonService.Decrypt(cipherEmailId);

                bool isUpdate = _newSecurityCaller.UpdatePassword(new SecurityService(_Cache,Db), encryptedEmailId, Password);

                if (isUpdate)
                {
                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                    _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                    _objResponseModel.ResponseData = "Update password successfully";
                }
            }
            catch (Exception)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                _objResponseModel.ResponseData = "Issue while update password";
            }
            return _objResponseModel;
        }

        #endregion

        #region Authenticate User
        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("authenticateUser")]
        [HttpPost]
        public ResponseModel authenticateUser()
        {
            string X_Authorized_Programcode = Convert.ToString(Request.Headers["X-Authorized-Programcode"]);
            string X_Authorized_userId = Convert.ToString(Request.Headers["X-Authorized-userId"]);
            string X_Authorized_password = Convert.ToString(Request.Headers["X-Authorized-password"]);
            string X_Authorized_Domainname = Convert.ToString(Request.Headers["X-Authorized-Domainname"]);

            ResponseModel resp = new ResponseModel();

            try
            {
                securityCaller _newSecurityCaller = new securityCaller();
                AccountModal account = new AccountModal();
                string Programcode = X_Authorized_Programcode.Replace(' ', '+');
                string Domainname = X_Authorized_Domainname.Replace(' ', '+');
                string userId = X_Authorized_userId.Replace(' ', '+');
                string password = X_Authorized_password.Replace(' ', '+');

                if (!string.IsNullOrEmpty(Programcode) && !string.IsNullOrEmpty(Domainname) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
                {
                    account = _newSecurityCaller.validateUser(new SecurityService(_Cache,Db), Programcode, Domainname, userId, password);

                    if (!string.IsNullOrEmpty(account.Token))
                    {
                        account.IsActive = true;
                        resp.Status = true;
                        resp.StatusCode = (int)EnumMaster.StatusCode.Success;
                        resp.ResponseData = account;
                        resp.Message = "Valid Login";
                    }
                    else
                    {
                        account.IsActive = false;
                        resp.Status = true;
                        resp.StatusCode = (int)EnumMaster.StatusCode.Success;
                        resp.ResponseData = account;
                        resp.Message = "In-Valid Login";
                    }
                }
                else
                {
                    resp.Status = false;
                    resp.ResponseData = account;
                    resp.Message = "Invalid Login";
                   
                }
            }
            catch (Exception _ex)
            {
                resp.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                resp.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                resp.ResponseData = "Message:" + Convert.ToString(_ex.Message) + "--- Inner Exception:" + Convert.ToString(_ex.InnerException)
                   + "Other:" + "Authenticate controller, " + Convert.ToString(_ex.Data);
            }
           
            return resp;
        }

        
        /// <summary>
        /// Logout User
        /// </summary>
        /// <returns></returns>
        [Route("Logout")]
        [HttpPost]
        public ResponseModel Logout()
        {
            ResponseModel resp = new ResponseModel();

            string _token = Convert.ToString(Request.Headers["X-Authorized-Token"]);
            _token = SecurityService.DecryptStringAES(_token);

            RedisCacheService radisCacheService = new RedisCacheService(_Cache);
            //if (!radisCacheService.Exists(_token))
            //{
            //    radisCacheService.Remove(_token);
            //}

            securityCaller _newSecurityCaller = new securityCaller();
            _newSecurityCaller.Logout(new SecurityService(_Cache,Db), _token);

            resp.Status = true;
            resp.StatusCode = (int)EnumMaster.StatusCode.Success;
            resp.ResponseData = null ;
            resp.Message = "Logout Successfully!";

            return resp;
        }
        

        /// <summary>
        /// Validate Program code 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("validateprogramcode")]
        [HttpGet]
        public ResponseModel validateprogramcode()
        {
            string X_Authorized_Programcode = Convert.ToString(Request.Headers["X-Authorized-Programcode"]);
            string X_Authorized_Domainname = Convert.ToString(Request.Headers["X-Authorized-Domainname"]);

            ResponseModel resp = new ResponseModel();
            securityCaller newSecurityCaller = new securityCaller();
            try
            {
                securityCaller _newSecurityCaller = new securityCaller();
                string Programcode = X_Authorized_Programcode.Replace(' ', '+');
                string Domainname = X_Authorized_Domainname.Replace(' ', '+');

                if (!string.IsNullOrEmpty(Programcode) && !string.IsNullOrEmpty(Domainname))
                {
                    bool isValid = newSecurityCaller.validateProgramCode(new SecurityService(_Cache,Db), Programcode, Domainname);

                    if (isValid)
                    {
                        resp.Status = true;
                        resp.StatusCode = (int)EnumMaster.StatusCode.Success;
                        resp.ResponseData = "";
                        resp.Message = "Valid Program code";
                    }
                    else
                    {
                        resp.Status = true;
                        resp.StatusCode = (int)EnumMaster.StatusCode.RecordNotFound;
                        resp.ResponseData = "";
                        resp.Message = "In-Valid Program code";
                    }
                }
                else
                {
                    resp.Status = false;
                    resp.ResponseData = "";
                    resp.Message = "In-valid Program code";
                }
            }
            catch (Exception _ex)
            {
                resp.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                resp.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                resp.ResponseData = "Message:" + Convert.ToString(_ex.Message) + "--- Inner Exception:" + Convert.ToString(_ex.InnerException)
                   + "Other:" + "Authenticate controller, " + Convert.ToString(_ex.Data);
            }           
            return resp;
        }

        #endregion

        #endregion

    }
}