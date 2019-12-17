using System;
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
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
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
        public ActionResult ForgetPassword(string emailToAddress= "shlob.barot@brainvire.com", string EmailId = "shlob.barot@brainvire.com", string subject= "Forget Password")
        {
            
            try
            {
                string gmailUserName = EmailId;
                string mailbody = "Hello DemoUser, This is link for change password";
                string gmailUserPassword = "Asdzxc#411";
                string SMTPSERVER = "smtp.gmail.com";
                int PORTNO = 587;

                SmtpClient smtpClient = new SmtpClient(SMTPSERVER, PORTNO);
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(gmailUserName, gmailUserPassword);
                {
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(gmailUserName, "Finocart");
                        message.Subject = subject == null ? "" : subject;
                        message.Body = mailbody;
                        message.IsBodyHtml = true;
                        message.To.Add(emailToAddress);

                        try
                        {
                            smtpClient.Send(message);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return null;
            
        }
        #endregion
    }
}