using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Easyrewardz_TicketSystem.Services
{
    public class CommonService
    {
        #region Variable Declaration
        public static string sLogFormat;
        public static string sErrorTime;
        public static string _connectionString;
        private readonly bool _SeeErrorLog;
        #endregion

        public CommonService()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            _connectionString = root.GetSection("ConnectionStrings").GetSection("DataAccessErMasterMySqlProvider").Value;
            _SeeErrorLog = Convert.ToBoolean(root.GetSection("ErrorLog").Value);
        }


        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="smtpDetails"></param>
        /// <param name="emailToAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public bool SendEmail(SMTPDetails smtpDetails, string emailToAddress, string subject, string body, string[] cc = null, string[] bcc = null, int tenantId = 0)
        {
            bool isMailSent = false;
            try
            {


                SmtpClient smtpClient = new SmtpClient(smtpDetails.SMTPServer, Convert.ToInt32(smtpDetails.SMTPPort))
                {
                    EnableSsl = smtpDetails.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(smtpDetails.FromEmailId, smtpDetails.Password)
                };
                {
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress(smtpDetails.FromEmailId, "EasyRewardz");

                        if (cc != null)
                        {
                            if (cc.Length > 0)
                            {
                                for (int i = 0; i < cc.Length; i++)
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
                if (_SeeErrorLog)
                {
                    ErrorLog errorLogs = new ErrorLog
                    {
                        ActionName = "SendEmail",
                        ControllerName = "CommonService",
                        TenantID = tenantId,
                        UserID = 0,
                        Exceptions = ex.StackTrace,
                        MessageException = ex.Message + " - " + ex.InnerException.ToString(),
                        IPAddress = emailToAddress
                    };
                    ErrorLogging errorLogging = new ErrorLogging(_connectionString);
                    errorLogging.InsertErrorLog(errorLogs);
                }
                throw;
            }
            return isMailSent;
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="Planetext"></param>
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText"></param>
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get details from the Token
        /// </summary>
        /// <param name="secreatetoken"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetTokenData(string secreatetoken)
        {
            ///As we have double encrypt token
            //1. De-Encrypt
            string seceateTokendata = SecurityService.DecryptStringAES(secreatetoken);

            Dictionary<string, string> tokenData = new Dictionary<string, string>();

            string[] splitstr = secreatetoken.Split('.');
            byte[] date = Convert.FromBase64String(splitstr[1]);
            byte[] contactdata = Convert.FromBase64String(splitstr[0]);
            string actualtoken_value = Encoding.ASCII.GetString(contactdata);
            string[] splitactualvalue = actualtoken_value.Split("_");

            tokenData.Add("ProgramCode", splitactualvalue[0]);
            tokenData.Add("DomainName", splitactualvalue[1]);
            tokenData.Add("AppId", splitactualvalue[2]);

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
        /// <param name="FilePath"></param>
        /// <returns></returns>
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
                                            dtCsv.Columns.Add(rowValues[j].Trim().Replace(" ", "")); //add headers  
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
            catch (Exception)
            {
                throw;
            }

            return dsCsv;

        }

        /// <summary>
        /// DataTable To Csv
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Save File
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="FileContent"></param>
        /// <returns></returns>
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
            string Password = "";
            try
            {

                string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
                string numbers = "1234567890";
                string SpecialCharacters = "!@#$%^&*?_-";

                string characters = "";
                //if (rbType.SelectedItem.Value == "1")
                {
                    characters += alphabets + small_alphabets + numbers + SpecialCharacters;
                }

                for (int i = 0; i < 8; i++)
                {
                    string character = string.Empty;
                    do
                    {
                        int index = new Random().Next(0, characters.Length);
                        character = characters.ToCharArray()[index].ToString();
                    } while (Password.IndexOf(character) != -1);
                    Password += character;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Password;
        }

        /// <summary>
        /// Create Log Files
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Error Log
        /// </summary>
        /// <param name="sPathName"></param>
        /// <param name="sErrMsg"></param>
        /// <returns></returns>
        public static void ErrorLog(string sPathName, string sErrMsg)
        {
            if (File.Exists(sPathName))
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


        /// <summary>
        /// SEND api request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string SendApiRequestOld(string url, string Request)
        {
            string strresponse = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "text/json";

                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    if (!string.IsNullOrEmpty(Request))
                        streamWriter.Write(Request);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    strresponse = streamReader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        strresponse = reader.ReadToEnd();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return strresponse;

        }

        public static string SendApiRequest(string url, string Request)
        {
            string strresponse = "";
            Stopwatch stopWatch = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    stopWatch = Stopwatch.StartNew();
                    HttpContent inputContent = new StringContent(Request, Encoding.UTF8, "text/json");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("text/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage responseMessage = client.PostAsync(url, inputContent).Result;
                    strresponse = responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        strresponse = reader.ReadToEnd();

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                stopWatch.Stop();
              //  SaveClientAPILog(url, Request, strresponse, stopWatch, "");
            }

            return strresponse;

        }

        /// <summary>
        /// Send Image API Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string SendImageApiRequestOld(string url, string Request)
        {
            string strresponse = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "text/json";

                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    if (!string.IsNullOrEmpty(Request))
                        streamWriter.Write(Request);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    strresponse = streamReader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        strresponse = reader.ReadToEnd();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return strresponse;

        }

        public static string SendImageApiRequest(string url, string Request)
        {
            string strresponse = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent inputContent = new StringContent(Request, Encoding.UTF8, "text/json");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("text/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage responseMessage = client.PostAsync(url, inputContent).Result;
                    strresponse = responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        strresponse = reader.ReadToEnd();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return strresponse;

        }

        /// <summary>
        /// Send Api Request Token
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string SendApiRequestTokenOld(string url, string Request, string token = "")
        {
            string strresponse = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                //httpWebRequest.Accept = "text/plain";

                httpWebRequest.ContentType = "application/x-www-form-urlencoded";


                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] byte1 = encoding.GetBytes(Request);

                //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                //{
                //    if (!string.IsNullOrEmpty(Request))
                //        streamWriter.Write(Request);
                //}
                Stream newStream = httpWebRequest.GetRequestStream();

                newStream.Write(byte1, 0, byte1.Length);
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    strresponse = streamReader.ReadToEnd();
                }

            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        strresponse = reader.ReadToEnd();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return strresponse;

        }

        public static string SendApiRequestToken(string url, string Request, string token = "")
        {
            string strresponse = "";
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] byte1 = encoding.GetBytes(Request);
                

                using (HttpClient client = new HttpClient())
                {
                    HttpContent inputContent = new StringContent(Request, Encoding.UTF8, "application/x-www-form-urlencoded");
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    HttpResponseMessage responseMessage = client.PostAsync(url, inputContent).Result;
                    strresponse = responseMessage.Content.ReadAsStringAsync().Result;
                }               

            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        strresponse = reader.ReadToEnd();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return strresponse;

        }

        /// <summary>
        /// Send Api Request Merchant Api
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string SendApiRequestMerchantApi(string url, string Request, string token = "")
        {
            string strresponse = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.Accept = "text/plain";

                httpWebRequest.ContentType = "application/json ";
                httpWebRequest.Headers.Add("Authorization", "Bearer " + token);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    if (!string.IsNullOrEmpty(Request))
                        streamWriter.Write(Request);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    strresponse = streamReader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        strresponse = reader.ReadToEnd();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return strresponse;

        }


        public static string SendParamsApiRequest(string url, NameValueCollection Request)
        {
            string strresponse = string.Empty;
            Stopwatch stopWatch = null;
            try
            {
                stopWatch = Stopwatch.StartNew();
                WebClient wc = new WebClient();
                wc.QueryString = Request;

                var data = wc.UploadValues(url, "POST", wc.QueryString);

                strresponse = UnicodeEncoding.UTF8.GetString(data);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                stopWatch.Stop();
                //SaveClientAPILog(url, JsonConvert.SerializeObject(Request.AllKeys.ToDictionary(x => x, x => Request[x])), strresponse, stopWatch, "");
            }

            return strresponse;


        }

        public static string SendParamsApiRequest(string url, Dictionary<string, string> Request)
        {
            string strresponse = string.Empty;
            try
            {
                string getQueryString = BuildQuerystring(Request);

                UriBuilder builder = new UriBuilder(url)
                {
                    Query = getQueryString
                };

                HttpClient client = new HttpClient();
                var result = client.PostAsync(builder.Uri, null).Result;

                using (StreamReader sr = new StreamReader(result.Content.ReadAsStreamAsync().Result))
                {
                    strresponse = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return strresponse;
        }

        public static string BuildQuerystring(Dictionary<string, string> querystringParams)
        {
            List<string> paramList = new List<string>();
            foreach (var parameter in querystringParams)
            {
                paramList.Add(parameter.Key + "=" + parameter.Value);
            }
            return "?" + string.Join("&", paramList);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /*
        public static async void SaveClientAPILog(string url, string Request, string response, Stopwatch stopWatch, string token = "")
        {
            
            int Result = 0; string ProgCode = string.Empty;
            string _connectionString = string.Empty;
            bool IsLog = false;
            try
            {
                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);
                var root = configurationBuilder.Build();
                _connectionString = root.GetSection("ConnectionStrings").GetSection("DataAccessErMasterMySqlProvider").Value;
                IsLog = Convert.ToBoolean(root.GetSection("SaveReqRespLog").Value);

                if (IsLog)
                {

               

                    if (Request.ToLower().Contains("programcode"))
                    {
                        ProgCode = Convert.ToString(JsonConvert.DeserializeObject<Dictionary<string, object>>(Request.ToLower())["programcode"]);
                    }
                    ErrorLogging Er = new ErrorLogging(_connectionString);

                    Result = await Er.InsertAPILog(new APILogModel()
                    {
                        ProgramCode = ProgCode,
                        ActionName = url.Split('/')[(url.Split('/').Length - 1)],
                        Method = "POST",
                        RequestUrl = url,
                        RequestBody = Request,
                        Response = response,
                        ResponseTimeTaken = Convert.ToInt32(stopWatch.ElapsedMilliseconds),
                        IsClientAPI = true

                    });

                }

            }
            catch (Exception)
            {
                throw;
            }
            
    }
    */
    }
}
