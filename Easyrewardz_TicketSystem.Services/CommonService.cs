using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
   public class CommonService
    {
        public void SendEmail(SMTPDetails smtpDetails, string emailToAddress, string subject, string body)
        {
            try
            {
                string gmailUserName = smtpDetails.FromEmailId;
                string gmailUserPassword = smtpDetails.Password;
                string smtpAddress = emailToAddress;
                string SMTPSERVER = smtpDetails.SMTPServer;
                int PORTNO = Convert.ToInt32(smtpDetails.SMTPPort);

                SmtpClient smtpClient = new SmtpClient(SMTPSERVER, PORTNO);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(gmailUserName, gmailUserPassword);
                {
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(gmailUserName, "EasyRewardz");
                        message.Subject = subject == null ? "" : subject;
                        message.Body = body == null ? "" : body;
                        message.IsBodyHtml = smtpDetails.IsBodyHtml;
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
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
