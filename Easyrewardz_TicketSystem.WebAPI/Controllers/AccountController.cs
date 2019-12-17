﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Easyrewardz_TicketSystem.WebAPI.Filters;
using Easyrewardz_TicketSystem.WebAPI.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    //[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public class AccountController : ControllerBase
    {
        //CommonServices _CommonRepository = new CommonServices();

        [AllowAnonymous]
        [HttpGet]
        public string authenticate()
        {
            Result resp = new Result();
            try
            {
                securityCaller _newSecurityCaller = new securityCaller();
                string Programcode = HttpContext.Request.Headers["X-Authorized-Programcode"];
                string Domainname = HttpContext.Request.Headers["X-Authorized-Domainname"];
                string applicationid = HttpContext.Request.Headers["X-Authorized-applicationid"];
                string userId = HttpContext.Request.Headers["X-Authorized-userId"];
                string password = HttpContext.Request.Headers["X-Authorized-password"];


                if (!string.IsNullOrEmpty(Programcode) && !string.IsNullOrEmpty(Domainname) && !string.IsNullOrEmpty(applicationid) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password))
                {
                    string token = _newSecurityCaller.generateToken(new SecurityService(), Programcode, applicationid, Domainname, userId, password);
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
            finally
            {
                GC.Collect();
            }

        }



        #region Forgot password screen
        /// <summary>
        /// Forgot password screen
        /// </summary>
        /// <returns></returns>
        //Send mail for Forgot Password
        [HttpPost]
        [AllowAnonymous]
        public JsonResult ForgetPassword(string EmailId)
        {

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
        [AllowAnonymous]
        public JsonResult UpdatePassword(string cipherEmailId, string Password)
        {
            try
            {
                securityCaller _newSecurityCaller = new securityCaller();
                
                bool isUpdate = _newSecurityCaller.UpdatePassword(cipherEmailId, Password);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return null;

        }
    }
}