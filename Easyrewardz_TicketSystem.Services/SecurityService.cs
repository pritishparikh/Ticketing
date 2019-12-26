using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.Services
{
    public class SecurityService : ISecurity
    {
        #region Variable Declartion 
        private readonly ETSContext _eContext;
        #endregion 

        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public SecurityService(string _connectionString)
        {
            conn.ConnectionString = _connectionString;
        }
        #endregion

        #region Encrypt -Decrypt Methods
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="DecryptStringAES"></param>
        /// <returns></returns>
        public static string DecryptStringAES(string cipherText)
        {

            var keybytes = Encoding.UTF8.GetBytes("sblw-3hn8-sqoy19");
            var iv = Encoding.UTF8.GetBytes("sblw-3hn8-sqoy19");

            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = Decrypt(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="DecryptStringFromBytes"></param>
        /// <returns></returns>
        private static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                try
                {
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="Encrypt"></param>
        /// <returns></returns>
        private static string Encrypt(string plainText)
        {

            var key = Encoding.UTF8.GetBytes("sblw-3hn8-sqoy19");
            var iv = Encoding.UTF8.GetBytes("sblw-3hn8-sqoy19");
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;


            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            string finaltoken = Convert.ToBase64String(encrypted, 0, encrypted.Length);

            // Return the encrypted bytes from the memory stream.
            return finaltoken;
        }
        #endregion

        #region Genrate Token 
        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="ProgramCode"></param>
        /// <param name="Domainname"></param>
        /// <param name="applicationid"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AccountModal getToken(string ProgramCode, string Domainname, string applicationid, string userId, string password)
        {
            //string _Token = "";
            AccountModal accountModals = new AccountModal();
            try
            {
                //ETSContext _DBContext = new ETSContext();

                string _Programcode = DecryptStringAES(ProgramCode);
                string _Domainname = DecryptStringAES(Domainname);
                string _applicationid = DecryptStringAES(applicationid);
                string _userId = DecryptStringAES(userId);
                string _password = DecryptStringAES(password);

                string sessionid = "";
                string newToken = generatetoken(_Programcode, _Domainname, _applicationid, _userId);
                 updatetocache(_userId, newToken);
                 SaveRecord(_Programcode, _Domainname, _applicationid, sessionid, _userId, _password, newToken);

                if (!string.IsNullOrEmpty(newToken) && !string.IsNullOrEmpty(_userId))
                {
                    accountModals.Token = newToken;
                    accountModals.Message = "Valid login";
                    accountModals.UserID = _userId;
                    accountModals.IsActive = true;
                    accountModals.LoginTime = DateTime.Now;
                }
                else
                {
                    accountModals.Message = "InValid login";

                }
                

                
                //_Token = newToken;
            }
            catch (Exception _ex)
            {
                throw _ex;
            }

            return accountModals;
        }
        #endregion

        #region Update to Redis
        /// <summary>
        /// Update to Redis
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="securittoken"></param>
        private void updatetocache(string userid, string securittoken)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("13.67.69.216:6379");
                IDatabase db = redis.GetDatabase();
                db.StringSet(userid, securittoken);
                string abc = db.StringGet(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region For Token Generation
        private string generatetoken(string Programcode, string Domainname, string applicationid, string userid)
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(Programcode + "_" + Domainname + "_" + applicationid);
                string token = Convert.ToBase64String(bytes);
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                string SecreateToken = Encrypt(token) + "." + Convert.ToBase64String(time.Concat(key).ToArray());

                return SecreateToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Validate security Token For Access permission
        public DataSet validateTokenGetPermission(string SecretToken, int ModuleID)
        {
            //ETSContext _DBContext = new ETSContext();
            DataSet ds = validateSecurityToken(SecretToken, ModuleID);
            return ds;
        }
        public DataSet validateSecurityToken(string SecretToken, int ModuleID)
        {
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("prc_validateToken", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Security_Token", SecretToken);
                cmd1.Parameters.AddWithValue("@Module_ID", ModuleID);
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null)
                {
                    var setoken = ds.Tables["CurrentSession"].Rows[0]["SecurityToken"];
                    var Module = ds.Tables["accessManagement"].Rows[0]["ModuleID"];
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return ds;
        }
        #endregion

        #region Update cache(Redis)
        private void updatecache(string userid, string securittoken)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("13.67.69.216:6379");
                IDatabase db = redis.GetDatabase();
                db.StringSet(userid, securittoken);
                string abc = db.StringGet(userid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Update Password
        public bool UpdatePassword(string EmailId, string Password)
        {
            try
            {
                //ETSContext _DBContext = new ETSContext();
                bool isUpdated = updatePassword(EmailId, Password);
                return isUpdated;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool updatePassword(string EmailId, string Password)
        {
            bool isUpdated = false;

            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_UpdatePassword", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@EmailId", EmailId);
                cmd1.Parameters.AddWithValue("@Password", Password);
                cmd1.ExecuteScalar();
                isUpdated = true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return isUpdated;
        }
        #endregion

        #region Send mail for forget password
        public bool sendMailForForgotPassword(string emailId, string content)
        {
            bool isSent = false;
            try
            {
                CommonService commonService = new CommonService();
                isSent = commonService.SendEmail(emailId, "Forgot Password", content, null, null);

                return isSent;
            }
            catch (Exception ex)
            {
                isSent = false;
            }

            return isSent;
        }
        #endregion

        #region Save Generated token 
        public string SaveRecord(string ProgramCode, string Domainname, string applicationid, string sessionid, string userId, string password, string newToken)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                //MySqlCommand cmd1 = new MySqlCommand("sp_insertER_CurrentSession", conn);
                MySqlCommand cmd1 = new MySqlCommand("prc_insertCurrentSession", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@UserName", userId);
                cmd1.Parameters.AddWithValue("@SecurityToken", newToken);
                cmd1.Parameters.AddWithValue("@SessionID", sessionid);
                cmd1.Parameters.AddWithValue("@ProgramCode", ProgramCode);
                cmd1.Parameters.AddWithValue("@Password", password);
                cmd1.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return "";
        }
        #endregion

        #region Commented code (pls dont Remove) 
        //private static string Decrypt(string EncptToken)
        //{
        //    try
        //    {
        //        var key = "sblw-3hn8-sqoy19";
        //        byte[] inputArray = Convert.FromBase64String(EncptToken.Replace(".", "+"));
        //        TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
        //        tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
        //        tripleDES.Mode = CipherMode.ECB;
        //        tripleDES.Padding = PaddingMode.PKCS7;
        //        ICryptoTransform cTransform = tripleDES.CreateDecryptor();
        //        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        //        tripleDES.Clear();
        //        var finalDecrptToken = UTF8Encoding.UTF8.GetString(resultArray);
        //        return finalDecrptToken;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        //private static string Encrypt(string token, byte[] key, byte[] iv)
        //{
        //    try
        //    {
        //        var key = "sblw-3hn8-sqoy19";
        //        byte[] inputArray = UTF8Encoding.UTF8.GetBytes(token);
        //        TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
        //        tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
        //        tripleDES.Mode = CipherMode.ECB;
        //        tripleDES.Padding = PaddingMode.PKCS7;
        //        ICryptoTransform cTransform = tripleDES.CreateEncryptor();
        //        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        //        tripleDES.Clear();
        //        var finaltoken = Convert.ToBase64String(resultArray, 0, resultArray.Length);
        //        return finaltoken;


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}
        #endregion
    }
}
