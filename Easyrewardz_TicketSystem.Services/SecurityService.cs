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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Easyrewardz_TicketSystem.Services
{
    public class SecurityService : ISecurity
    {
        #region Variable Declartion 
        private readonly string radisCacheServerAddress;
        #endregion 

        #region Constructor
        MySqlConnection conn = new MySqlConnection();
        public SecurityService(string _connectionString, string _radisCacheServerAddress = null)
        {
            conn.ConnectionString = _connectionString;
            radisCacheServerAddress = _radisCacheServerAddress;
        }
        #endregion

        #region Custom Methods

        #region Encrypt -Decrypt Methods (AES)

        /// <summary>
        /// Decrypt String from Cipher text
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
        /// Decrypt string which call from DecryptStringAES
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
        /// Encrypt string from plain text
        /// </summary>
        /// <param name="Encrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
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

        #region Validate security Token For Access permission
        public DataSet validateTokenGetPermission(string SecretToken, int ModuleID)
        {
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

        #region Update Password

        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool UpdatePassword(string EmailId, string Password)
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

        #region Login/Authenticate Methods

        /// <summary>
        /// Authenticate User for first login
        /// </summary>
        /// <param name="Program_Code"> Program Code </param>
        /// <param name="Domain_Name"> Domain Name </param>
        /// <param name="User_EmailID"> User EmailID </param>
        /// <param name="User_Password"> User Password </param>
        /// <returns>Authenticate</returns>
        public AccountModal AuthenticateUser(string Program_Code, string Domain_Name, string User_EmailID, string User_Password)
        {
            AccountModal accountModal = new AccountModal();

            try
            {
                ////Decrypt Data 
                Program_Code = DecryptStringAES(Program_Code);
                Domain_Name = DecryptStringAES(Domain_Name);
                User_EmailID = DecryptStringAES(User_EmailID);

                Authenticate authenticate = new Authenticate();
                ////Check whether Login is valid or not
                authenticate = isValidLogin(Program_Code, Domain_Name, User_EmailID, User_Password);

                if (authenticate.UserMasterID > 0)
                {
                    /*Valid User then generate token and save to the database */

                    ////Generate Token 
                    string _token = generateAuthenticateToken(authenticate.ProgramCode, authenticate.Domain_Name, authenticate.AppID);

                    authenticate.Token = _token;

                    //Save User Token
                    SaveUserToken(authenticate);

                    //Serialise Token & save token to Cache 
                    string jsonString = JsonConvert.SerializeObject(authenticate);

                    RedisCacheService radisCacheService = new RedisCacheService(radisCacheServerAddress);
                    radisCacheService.Set(authenticate.Token, jsonString);

                    accountModal.Message = "Valid user";

                    ////Double encryption: We are doing encryption of encrypted token 
                    accountModal.Token = Encrypt(_token);
                    accountModal.IsValidUser = true;
                }
                else
                {
                    //Wrong Username or password
                    accountModal.Message = "Invalid username or password";
                    accountModal.Token = "";
                    accountModal.IsValidUser = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            return accountModal;
        }

        private Authenticate isValidLogin(string Program_Code, string Domain_Name, string User_EmailID, string User_Password)
        {
            MySqlCommand cmd = new MySqlCommand();
            Authenticate authenticate = new Authenticate();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_ValidateUserLogin", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Program_Code", Program_Code);
                cmd1.Parameters.AddWithValue("@Domain_Name", Domain_Name);
                cmd1.Parameters.AddWithValue("@User_EmailID", User_EmailID);
                cmd1.Parameters.AddWithValue("@User_Password", User_Password);

                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.SelectCommand = cmd1;
                da.Fill(ds);
                if (ds != null && ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        bool status = Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"]);

                        if (status)
                        {
                            authenticate.AppID = Convert.ToString(ds.Tables[0].Rows[0]["ApplicationId"]);
                            authenticate.ProgramCode = Program_Code;
                            authenticate.Domain_Name = Domain_Name;
                            authenticate.UserMasterID = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"]);
                            authenticate.TenantId = Convert.ToInt32(ds.Tables[0].Rows[0]["Tenant_Id"]);
                            authenticate.Message = "Valid user";
                        }
                        else
                        {
                            authenticate.AppID = "";
                            authenticate.ProgramCode = "";
                            authenticate.Domain_Name = "";
                            authenticate.Message = "In-valid username or passowrd";
                            authenticate.Token = "";
                            authenticate.UserMasterID = 0;
                            authenticate.TenantId = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return authenticate;
        }

        /// <summary>
        /// Generate Secure Token
        /// </summary>
        /// <param name="Programcode"></param>
        /// <param name="Domainname"></param>
        /// <param name="applicationid"></param>
        /// <returns></returns>
        private string generateAuthenticateToken(string Programcode, string Domainname, string applicationid)
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

        /// <summary>
        /// Save user token to current session
        /// </summary>
        /// <param name="accountModal"></param>
        /// <returns></returns>
        private Authenticate SaveUserToken(Authenticate authenticate)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_createCurrentSession", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@UserMaster_ID", authenticate.UserMasterID);
                cmd1.Parameters.AddWithValue("@Security_Token", authenticate.Token);
                cmd1.Parameters.AddWithValue("@App_Id", authenticate.AppID);
                cmd1.Parameters.AddWithValue("@Program_Code", authenticate.ProgramCode);
                cmd1.Parameters.AddWithValue("@Tenant_Id", authenticate.TenantId);
                cmd1.ExecuteNonQuery();
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
            return authenticate;
        }

        ///// <summary>
        ///// Set data to Radhish Cache memory
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="Value"></param>
        ////public void setRadishCacheData(string key, string Value)
        ////{
        ////    //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("13.67.69.216:6379");
        ////    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(radisCacheServerAddress);
        ////    IDatabase db = redis.GetDatabase();
        ////    db.StringSet(key, Value);
        ////}

        /////// <summary>
        /////// Get Data from the Radish cache memory
        /////// </summary>
        /////// <param name="key"></param>
        /////// <returns></returns>
        ////public string getDataFromRadishCache(string key)
        ////{
        ////    //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("13.67.69.216:6379");
        ////    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(radisCacheServerAddress);
        ////    IDatabase _db = redis.GetDatabase();
        ////    return _db.StringGet(key);
        ////}

        ////public void removeDataFromRadishCache(string key)
        ////{
        ////    //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("13.67.69.216:6379");
        ////    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(radisCacheServerAddress);
        ////    IDatabase _db = redis.GetDatabase();
        ////    _db.KeyDelete(key);
        ////}

        ////public bool checkDataExistInRadishCache(string key)
        ////{
        ////    //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("13.67.69.216:6379");
        ////    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(radisCacheServerAddress);
        ////    IDatabase _db = redis.GetDatabase();
        ////    return _db.KeyExists(key);
        ////}

        /// <summary>
        /// Logout user
        /// </summary>
        /// <param name="token_data">Token Data</param>
        public void Logout(string token_data)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                MySqlCommand cmd1 = new MySqlCommand("SP_LogoutUser", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@token_data", token_data);
                cmd1.ExecuteNonQuery();
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
        }

        #endregion

        /// <summary>
        /// Get data from token (Radish)
        /// </summary>
        /// <param name="_radisCacheServerAddress"></param>
        /// <param name="_token"></param>
        /// <returns></returns>
        public static Authenticate GetAuthenticateDataFromToken(string _radisCacheServerAddress,string _token)
        {
            Authenticate authenticate = new Authenticate();

            try
            {
                RedisCacheService cacheService = new RedisCacheService(_radisCacheServerAddress);
                if (cacheService.Exists(_token))
                {
                    string _data = cacheService.Get(_token);
                    authenticate = JsonConvert.DeserializeObject<Authenticate>(_data);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return authenticate;
        }

        #endregion

    }
}

