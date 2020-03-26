﻿using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Easyrewardz_TicketSystem.Services
{
    public class CommonService
    {
        public static string sLogFormat;
        public static string sErrorTime;
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

                        if(cc!=null)
                        {
                            if(cc.Length >0)
                            {
                                for(int i=0; i< cc.Length;i++)
                                {
                                    message.CC.Add(cc[i]);
                                }
                            
                            }
                        }

                        if (bcc != null)
                        {
                            if (bcc.Length > 0)
                            {
                                for (int k = 0; k < bcc.Length; k++)
                                {
                                    message.CC.Add(bcc[k]);
                                }

                            }
                        }
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

            string[] splitStr = secreatetoken.Split('.');
            byte[] date = Convert.FromBase64String(splitStr[1]);
            byte[] contactData = Convert.FromBase64String(splitStr[0]);
            string actualtoken_value = Encoding.ASCII.GetString(contactData);
            string[] splitactualValue = actualtoken_value.Split("_");

            tokenData.Add("ProgramCode", splitactualValue[0]);
            tokenData.Add("DomainName", splitactualValue[1]);
            tokenData.Add("AppId", splitactualValue[2]);

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

            // This is testing 
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


        /// <summary>
        /// Convert CSV to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>



        public static DataSet csvToDataSet(string FilePath)
        {
            DataTable dtCsv = new DataTable();
            string FileContent = string.Empty;
            DataSet dsCsv = new DataSet();
       
     
            try
            {
                if (!string.IsNullOrEmpty(FilePath))
                {
                    using (StreamReader sr = new StreamReader(FilePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            FileContent = sr.ReadToEnd().ToString(); //read full file text  
                            string[] rows = FileContent.Split('\n'); //split full file text into rows  
                            for (int i = 0; i < rows.Count() - 1; i++)
                            {
                                string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                                {
                                    if (i == 0)
                                    {
                                        for (int j = 0; j < rowValues.Count(); j++)
                                        {
                                            dtCsv.Columns.Add(rowValues[j].Trim().Replace(" ","")); //add headers  
                                        }
                                        
                                    }
                                    else
                                    {
                                        DataRow dr = dtCsv.NewRow();
                                        for (int k = 0; k < rowValues.Count(); k++)
                                        {
                                            dr[k] = rowValues[k].ToString().Trim();
                                        }
                                      
                                        dtCsv.Rows.Add(dr); //add other rows  

                                    }
                                }
                            }
                        }
                    }

                    dsCsv.Tables.Add(dtCsv);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return dsCsv;

        }

        public static string DataTableToCsv(DataTable table)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = table.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in table.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString();
        }

        public static bool SaveFile(string filepath, string FileContent)
        {


            string lastFolderName = Path.GetDirectoryName(filepath);
            if (!Directory.Exists(lastFolderName))
            {
                Directory.CreateDirectory(lastFolderName);
            }
            File.WriteAllText(filepath, FileContent);

            return true;
        }

        #region Generate random password
        /// <summary>
        /// Generating random password
        /// </summary>
        /// <returns></returns>
        public static string GeneratePassword()
        {
            string password = "";
            try
            {

                string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
                string numbers = "1234567890";
                string specialCharacters = "!@#$%^&*?_-";

                string characters = "";
                //if (rbType.SelectedItem.Value == "1")
                {
                    characters += alphabets + small_alphabets + numbers + specialCharacters;
                }

                for (int i = 0; i < 8; i++)
                {
                    string character = string.Empty;
                    do
                    {
                        int index = new Random().Next(0, characters.Length);
                        character = characters.ToCharArray()[index].ToString();
                    } while (password.IndexOf(character) != -1);
                    password += character;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return password;
        }

        public static void CreateLogFiles()
        {
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorTime = sYear + sMonth + sDay;
        }
        

        public static void ErrorLog(string sPathName, string sErrMsg)
        {
            if( File.Exists(sPathName))
            {
                StreamWriter sw = new StreamWriter(sPathName + sErrorTime, true);
                sw.WriteLine(sLogFormat + sErrMsg);
                sw.Flush();
                sw.Close();
            }
            else
            {
                StreamWriter stwriter = File.CreateText(sPathName);
                stwriter.WriteLine(sLogFormat + sErrMsg);
                stwriter.Flush();
                stwriter.Close();
            }

        }
        #endregion


    }
}
