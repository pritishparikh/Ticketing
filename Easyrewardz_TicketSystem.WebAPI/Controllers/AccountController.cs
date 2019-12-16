using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easyrewardz_TicketSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("*")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Common Data Repository
        ///// </summary>
        //private readonly ICommon _CommonRepository;

        //private readonly ILookupDetails _lookUpRepository;


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