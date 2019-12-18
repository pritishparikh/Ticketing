using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Data;

namespace Easyrewardz_TicketSystem.Services
{
    public class SecurityService : ISecurity
    {
        #region Declartion 

        /// <summary>
        /// Context
        /// </summary>
        private readonly ETSContext _eContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        //public SecurityService(ETSContext context)
        //{
        //    _eContext = context;
        //}

        #endregion

        public string getToken(string ProgramCode, string Domainname, string applicationid, string userId, string password)
        {
            string _Token = "";
            try
            {

                ETSContext _DBContext = new ETSContext();

                string _Programcode = Decrypt(ProgramCode);
                string _Domainname = Decrypt(Domainname);
                string _applicationid = Decrypt(applicationid);
                string _userId = Decrypt(userId);
                string _password = Decrypt(password);

                string sessionid = "";
                string newToken = generatetoken(_Programcode, _Domainname, _applicationid, _userId);
                _DBContext.SaveRecord(_Programcode, _Domainname, _applicationid, sessionid, _userId, _password, newToken);
                _Token = newToken;
            }
            catch (Exception _ex) { }

            return _Token;
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private static string Encrypt(string token)
        {
            try
            {
                var key = "sblw-3hn8-sqoy19";
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(token);
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
        private static string Decrypt(string EncptToken)
        {
            try
            {
                var key = "sblw-3hn8-sqoy19";
                byte[] inputArray = Convert.FromBase64String(EncptToken.Replace(".", "+"));
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

        private string generatetoken(string Programcode, string Domainname, string applicationid, string userid)
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(Programcode + "_" + Domainname + "_" + applicationid);
                string token = Convert.ToBase64String(bytes);
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                string SecreateToken = Encrypt(token) + "." + Convert.ToBase64String(time.Concat(key).ToArray());

                updatecache(userid, SecreateToken);

                return SecreateToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validate token and get permissions
        /// Later will pass the module Id
        /// </summary>
        /// <param name="SecretToken"></param>
        /// <returns></returns>
        public DataSet validateTokenGetPermission(string SecretToken,int ModuleID)
        {
            ETSContext _DBContext = new ETSContext();
            DataSet ds = _DBContext.validateSecurityToken(SecretToken,ModuleID);
            return ds;
        }

        #region Update cache
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

        public bool UpdatePassword(string EmailId,string Password)
        {
            ETSContext _DBContext = new ETSContext();
            bool isUpdated = _DBContext.updatePassword(EmailId, Password);
            return isUpdated;
        }
        #endregion
    }
}
