
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    /// <summary>
    /// Security 
    /// </summary>
    public class securityCaller
    {
        #region Variable Declaration
        private ISecurity _SecurityRepository;
        #endregion

        #region Custom Methods

        /// <summary>
        /// Generate token for the security
        /// </summary>
        /// <param name="security"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="Domainname"></param>
        /// <param name="applicationid"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AccountModal generateToken(ISecurity security, string ProgramCode, string Domainname, string applicationid, string userId, string password)
        {
            _SecurityRepository = security;
            return _SecurityRepository.getToken(ProgramCode, applicationid, Domainname, userId, password);
        }

        /// <summary>
        /// Validate token and get permission
        /// </summary>
        /// <param name="security"></param>
        /// <param name="SecretToken"></param>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public DataSet validateTokenGetPermission(ISecurity security, string SecretToken, int ModuleID)
        {
            _SecurityRepository = security;
            return _SecurityRepository.validateTokenGetPermission(SecretToken, ModuleID);
        }

        /// <summary>
        /// Update password 
        /// </summary>
        /// <param name="security">Interface </param>
        /// <param name="cipherEmailId">Encrypted email Id</param>
        /// <param name="Password">Plain text Password </param>
        /// <returns></returns>
        public bool UpdatePassword(ISecurity security, string cipherEmailId, string Password)
        {
            _SecurityRepository = security;
            CommonService objSmdService = new CommonService();
            string plainEmailId = objSmdService.Decrypt(cipherEmailId);
            string encryptedPassword = objSmdService.Encrypt(Password);

            return _SecurityRepository.UpdatePassword(plainEmailId, encryptedPassword);
        }

        /// <summary>
        /// Send Mail 
        /// </summary>
        /// <param name="security">Interface </param>
        /// <param name="cipherEmailId">Encrypted email Id</param>
        /// <param name="Password">Plain text Password </param>
        /// <returns></returns>
        public bool sendMail(ISecurity security, string EmailId, string content)
        {
            _SecurityRepository = security;
            CommonService commonService = new CommonService();


            return _SecurityRepository.sendMailForForgotPassword(EmailId, content);
        }


        /// <summary>
        /// Validate User Account
        /// </summary>
        /// <param name="security"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="Domainname"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AccountModal validateUser (ISecurity security, string ProgramCode, string Domainname, string userId, string password)
        {
            _SecurityRepository = security;
            return _SecurityRepository.AuthenticateUser(ProgramCode, Domainname, userId, password);
        }

        #endregion
    }
}
