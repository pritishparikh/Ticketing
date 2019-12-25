using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IConfiguration configuration;
        private readonly string _connectioSting;

        public AccountController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
            _connectioSting = configuration.GetValue<string>("ConnectionStrings:DataAccessMySqlProvider");
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public ResponseModel authenticate(string X_Authorized_Programcode, string X_Authorized_Domainname, string X_Authorized_applicationid, string X_Authorized_userId, string X_Authorized_password)
        //public string authenticate()
        {
            ResponseModel resp = new ResponseModel();
            try
            {
                securityCaller _newSecurityCaller = new securityCaller();
                AccountModal objAccount = new AccountModal();
                //string Programcode = HttpContext.Request.Headers["X_Authorized_Programcode"];
                //string Domainname = HttpContext.Request.Headers["X_Authorized_Domainname"];
                //string applicationid = HttpContext.Request.Headers["X_Authorized_applicationid"];
                //string userId = HttpContext.Request.Headers["X_Authorized_userId"];
                //string password = HttpContext.Request.Headers["X_Authorized_password"];
                string Programcode = X_Authorized_Programcode.Replace(' ', '+');
                string Domainname = X_Authorized_Domainname.Replace(' ','+');
                string applicationid = X_Authorized_applicationid.Replace(' ', '+');
                string userId = X_Authorized_userId.Replace(' ', '+');
                string password = X_Authorized_password.Replace(' ', '+');

                

                if (!string.IsNullOrEmpty(Programcode) && !string.IsNullOrEmpty(Domainname) && !string.IsNullOrEmpty(applicationid) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
                {
                    string token = _newSecurityCaller.generateToken(new SecurityService(_connectioSting), Programcode, applicationid, Domainname, userId, password);

                    objAccount.Message = "Valid login";
                    objAccount.Token = token;

                    resp.StatusCode = (int)EnumMaster.StatusCode.Success;
                    resp.ResponseData = objAccount;
                }
                else
                {
                    ///In valid code here
                    
                }
            }
            catch (Exception _ex)
            {
                resp.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                resp.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
            }
            finally
            {
                GC.Collect();
            }
            return resp;
        }


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
                CommonService objSmdService = new CommonService();
                string encryptedEmailId = objSmdService.Encrypt(EmailId);
                //SMTPDetails objSmtpDetail = new SMTPDetails();

                //string subject = "Forgot Password";
                //string url = configuration.GetValue<string>("websiteURL") + "/changePassword";
                //string body = "Hello, This is Demo Mail for testing purpose. <br/>" + url + "?Id=" + encryptedEmailId;
                //objSmtpDetail.FromEmailId = "shlok.barot@brainvire.com";
                //objSmtpDetail.Password = "Brainvire@2019";
                //objSmtpDetail.SMTPServer = "smtp.gmail.com";
                //objSmtpDetail.SMTPPort = 587;
                //objSmtpDetail.IsBodyHtml = true;

                //objSmdService.SendEmail(objSmtpDetail, EmailId, subject, body);

                securityCaller _securityCaller = new securityCaller();
                string url = configuration.GetValue<string>("websiteURL") + "/changePassword";
                string body = "Hello, This is Demo Mail for testing purpose. <br/>" + url + "?Id=" + encryptedEmailId;
                bool isUpdate = _securityCaller.sendMail(new SecurityService(_connectioSting), EmailId, body);

                if (isUpdate)
                {
                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                    _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                    _objResponseModel.ResponseData = "Mail sent successfully";
                }
                else
                {
                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                    _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                    _objResponseModel.ResponseData = "Mail sent failure";
                }
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                _objResponseModel.ResponseData = "Mail sent failure";
            }
            return _objResponseModel;
        }

        #endregion
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
                bool isUpdate = _newSecurityCaller.UpdatePassword(new SecurityService(_connectioSting),cipherEmailId, Password);

                if (isUpdate)
                {
                    _objResponseModel.Status = true;
                    _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.Success;
                    _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.Success);
                    _objResponseModel.ResponseData = "Update password successfully";
                }
            }
            catch (Exception ex)
            {
                _objResponseModel.Status = true;
                _objResponseModel.StatusCode = (int)EnumMaster.StatusCode.InternalServerError;
                _objResponseModel.Message = CommonFunction.GetEnumDescription((EnumMaster.StatusCode)(int)EnumMaster.StatusCode.InternalServerError);
                _objResponseModel.ResponseData = "Issue while update password";
            }
            return _objResponseModel;
        }
    }
}