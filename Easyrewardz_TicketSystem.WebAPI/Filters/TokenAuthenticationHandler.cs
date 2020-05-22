using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Filters
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
    {
        private const string SchemeName = "TokenAuth";
        private readonly string _radisCacheServerAddress;
        public IConfiguration Configurations { get; }
        public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IConfiguration configuration)
                : base(options, logger, encoder, clock)
        {
            _radisCacheServerAddress = configuration.GetValue<string>("radishCache");
            Configurations = configuration;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.Run(() => Authenticate());
        }
        private AuthenticateResult Authenticate()
        {

            //ETSContext _DBContext = new ETSContext();
            string token = Context.Request.Headers["X-Authorized-Token"];
            string userId = Context.Request.Headers["X-Authorized-userId"];
            if (token == null) return AuthenticateResult.Fail("No Authorization token provided");
            try
            {
                //string _userId = Decrypt(userId);
                //string isValidToken = validatetoken(token, _userId);

                //if (isValidToken == "1")
                //{
                //    var claims = new[] { new Claim(ClaimTypes.Name, isValidToken) };
                //    var identity = new ClaimsIdentity(claims, Scheme.Name);
                //    var principal = new ClaimsPrincipal(identity);
                //    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                //    return AuthenticateResult.Success(ticket);

                //}
                //else
                //{
                //    return AuthenticateResult.Fail("Invalid Authorization");
                //}

                var routeData = Context.Request.Path.Value;
                //var XAuthorizedToken = Convert.ToString(context.Request.Headers["X-Authorized-Token"]);
                if (!string.IsNullOrEmpty(routeData))
                {
                    if (!routeData.Contains("dev-Ticketingsecuritymodule"))
                    {
                        if (!routeData.Contains("validateprogramcode"))
                        {
                            var XAuthorizedProgramcode = Convert.ToString(Context.Request.Headers["X-Authorized-Programcode"]);
                            if (string.IsNullOrEmpty(XAuthorizedProgramcode))
                            {
                                var XAuthorizedToken = Convert.ToString(Context.Request.Headers["X-Authorized-Token"]);

                                Authenticate authenticates = new Authenticate();
                                authenticates = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(XAuthorizedToken));
                                XAuthorizedProgramcode = authenticates.ProgramCode;
                            }
                            else
                            {
                                XAuthorizedProgramcode = SecurityService.DecryptStringAES(XAuthorizedProgramcode);
                            }
                            if (XAuthorizedProgramcode != null)
                            {
                                RedisCacheService cacheService = new RedisCacheService(_radisCacheServerAddress);
                                if (cacheService.Exists("Con" + XAuthorizedProgramcode))
                                {
                                    string _data = cacheService.Get("Con" + XAuthorizedProgramcode);
                                    _data = JsonConvert.DeserializeObject<string>(_data);
                                    Configurations["ConnectionStrings:DataAccessMySqlProvider"] = _data;
                                }
                            }
                        }
                    }
                }


                Authenticate authenticate = SecurityService.GetAuthenticateDataFromToken(_radisCacheServerAddress, SecurityService.DecryptStringAES(token));

                if (!string.IsNullOrEmpty(authenticate.Token))
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, "1") };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);

                }
                else
                {
                    return AuthenticateResult.Fail("Invalid Authorization");
                }
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Failed to validate token");
            }
        }
        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
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
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }
        private string validatetoken(string secreatetoken, string userId)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("13.67.69.216:6379");
            IDatabase db = redis.GetDatabase();
            string UserFromRadisToken = db.StringGet(userId);

            string Newtoken = "";
            try
            {
                if (UserFromRadisToken == secreatetoken)
                {

                    string[] _splitstr = secreatetoken.Split('.');
                    byte[] date = Convert.FromBase64String(_splitstr[1]);
                    byte[] contactdata = Convert.FromBase64String(_splitstr[0]);
                    string actualtoken_value = Encoding.ASCII.GetString(contactdata);
                    string[] _splitactualvalue = actualtoken_value.Split("_");
                    if (!string.IsNullOrEmpty(_splitactualvalue[0]) || !string.IsNullOrEmpty(_splitactualvalue[1]) || !string.IsNullOrEmpty(_splitactualvalue[2]))
                    {
                        DateTime when = DateTime.FromBinary(BitConverter.ToInt64(date, 0));
                        if (when < DateTime.UtcNow.AddHours(-24))
                        {
                            Newtoken = generatetoken(_splitactualvalue[0], _splitactualvalue[1], _splitactualvalue[2], userId);
                        }
                        else
                        {
                            return "1";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Newtoken;
        }
        private string generatetoken(string Programcode, string Domainname, string applicationid, string userid)
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(Programcode + "_" + Domainname + "_" + applicationid);
                string token = Convert.ToBase64String(bytes);
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                string SecreateToken = token + "." + Convert.ToBase64String(time.Concat(key).ToArray());

                updatecache(userid, SecreateToken);

                return SecreateToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
        //public DataSet validateSecurityToken(string SecretToken,int ModuleID)
        //{
        //    ETSContext _DBContext = new ETSContext();
        //    DataSet ds = _DBContext.validateSecurityToken(SecretToken, ModuleID);
        //    return ds;

        //}
    }
    public static class SchemesNamesConst
    {
        public const string TokenAuthenticationDefaultScheme = "TokenAuthenticationScheme";

    }
    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {

    }
}
