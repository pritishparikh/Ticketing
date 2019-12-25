using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class CommonService
    { 
        /// <summary>
      /// Send Email
      /// </summary>
      /// <param name="smtpDetails"></param>
      /// <param name="emailToAddress"></param>
      /// <param name="subject"></param>
      /// <param name="body"></param>
      /// <returns></returns>
        public bool SendEmail(string emailToAddress, string subject, string body,string[] cc = null, string[] bcc = null)
        {
            bool isMailSent = false;
            try
            {
                SMTPDetails smtpDetails = new SMTPDetails();
                smtpDetails.FromEmailId = "realtester2019@gmail.com";
                smtpDetails.Password = "Brain@2019";
                smtpDetails.SMTPServer = "smtp.gmail.com";
                smtpDetails.SMTPPort = 587;
                smtpDetails.IsBodyHtml = true;

                string gmailUserName = smtpDetails.FromEmailId;
                string gmailUserPassword = smtpDetails.Password;
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
                        
                            smtpClient.Send(message);
                    }
                }

                isMailSent = true;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return isMailSent;
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string Encrypt(string Planetext)
        {
            try
            {
                var key = "sblw-3hn8-sqoy19";
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(Planetext);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                var finaltoken = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                return finaltoken;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="EncptToken"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText)
        {
            try
            {
                var key = "sblw-3hn8-sqoy19";
                byte[] inputArray = Convert.FromBase64String(cipherText.Replace(".", "+"));
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                var finalDecrptToken = UTF8Encoding.UTF8.GetString(resultArray);
                return finalDecrptToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
