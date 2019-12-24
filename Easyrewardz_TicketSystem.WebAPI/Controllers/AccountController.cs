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
                string Programcode = X_Authorized_Programcode;
                string Domainname = X_Authorized_Domainname;
                string applicationid = X_Authorized_applicationid;
                string userId = X_Authorized_userId;
                string password = X_Authorized_password;

                

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
        public JsonResult ForgetPassword(string EmailId)
        {
            //string EmailId = email;
            try
            {
                CommonService objSmdService = new CommonService();
                string encryptedEmailId = objSmdService.Encrypt(EmailId);
                SMTPDetails objSmtpDetail = new SMTPDetails();

                string subject = "Forgot Password";
                string url = "http://easyrewardz.brainvire.net/changePassword";
                string body = "Hello, This is Demo Mail for testing purpose. <br/>" + url + "?Id=" + encryptedEmailId;
                objSmtpDetail.FromEmailId = "shlok.barot@brainvire.com";
                objSmtpDetail.Password = "Brainvire@2019";
                objSmtpDetail.SMTPServer = "smtp.gmail.com";
                objSmtpDetail.SMTPPort = 587;
                objSmtpDetail.IsBodyHtml = true;

                objSmdService.SendEmail(objSmtpDetail, EmailId, subject, body);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return null;

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
        public JsonResult UpdatePassword(string cipherEmailId, string Password)
        {
            try
            {
                securityCaller _newSecurityCaller = new securityCaller();
                
                bool isUpdate = _newSecurityCaller.UpdatePassword(new SecurityService(_connectioSting),cipherEmailId, Password);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return null;

        }
    }
}