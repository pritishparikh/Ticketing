using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool SendEmail(SMTPDetails smtpDetails,string emailToAddress, string subject, string body, string[] cc = null, string[] bcc = null, int tenantId= 0)
        {
            bool isMailSent = false;
            try
            {
                //SMTPDetails smtpDetails = new SMTPDetails();
                
                //smtpDetails.FromEmailId = "realtester2019@gmail.com";
                //smtpDetails.Password = "Brain@2019";
                //smtpDetails.SMTPServer = "smtp.gmail.com";
                //smtpDetails.SMTPPort = "587";
                //smtpDetails.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient(smtpDetails.SMTPServer, Convert.ToInt32(smtpDetails.SMTPPort));
                smtpClient.EnableSsl = smtpDetails.EnableSsl;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(smtpDetails.FromEmailId, smtpDetails.Password);
                {
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(smtpDetails.FromEmailId, "EasyRewardz");
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

        /// <summary>
        /// Get details from the Token
        /// </summary>
        /// <param name="secreatetoken"></param>
        /// <returns></returns>
        public Dictionary<string, string> getTokenData(string secreatetoken)
        {
            ///As we have double encrypt token
            //1. De-Encrypt
            string seceateTokendata = SecurityService.DecryptStringAES(secreatetoken);

            Dictionary<string, string> tokenData = new Dictionary<string, string>();

            string[] _splitstr = secreatetoken.Split('.');
            byte[] date = Convert.FromBase64String(_splitstr[1]);
            byte[] contactdata = Convert.FromBase64String(_splitstr[0]);
            string actualtoken_value = Encoding.ASCII.GetString(contactdata);
            string[] _splitactualvalue = actualtoken_value.Split("_");

            tokenData.Add("ProgramCode", _splitactualvalue[0]);
            tokenData.Add("DomainName", _splitactualvalue[1]);
            tokenData.Add("AppId", _splitactualvalue[2]);

            return tokenData;
        }
        /// <summary>
        /// Export List as CSV
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="ExcludeColumns">Pass comma separated column names</param>
        /// <returns></returns>
        public static string ListToCSV<T>(IEnumerable<T> list, string ExcludeColumns = "")
        {

            StringBuilder sList = new StringBuilder();

            Type type = typeof(T);
            var props = type.GetProperties();


            if (string.IsNullOrEmpty(ExcludeColumns))
            {
                sList.Append(string.Join(",", props.Select(p => p.Name)));
            }
            else
            {
                foreach (var propertyInfo in props)
                {
                    if (!string.IsNullOrEmpty(ExcludeColumns))
                    {
                        if (!ExcludeColumns.ToLower().Contains(propertyInfo.Name.ToLower()))
                        {
                            if (sList.Length == 0)
                            {
                                sList.Append(propertyInfo.Name);
                            }
                            else
                            {
                                sList.Append("," + propertyInfo.Name);
                            }
                        }
                    }
                }
            }
            string customCOlumns = sList.ToString();

            sList.Append(Environment.NewLine);


            foreach (var element in list)
            {
                if (string.IsNullOrEmpty(ExcludeColumns))
                {
                    sList.Append(string.Join(",", props.Select(p => p.GetValue(element, null))));
                }
                else
                {
                    int j = 0;
                    foreach (var propertyInfo in props)
                    {
                        if (!ExcludeColumns.ToLower().Contains(propertyInfo.Name.ToLower()))
                        {
                            if (j == 0)
                            {
                                sList.Append(propertyInfo.GetValue(element, null));
                                j = j + 1;
                            }
                            else
                            {
                                sList.Append("," + propertyInfo.GetValue(element, null));
                            }
                        }
                    }
                }
                sList.Append(Environment.NewLine);
            }
            return sList.ToString();
        }
    }
}
