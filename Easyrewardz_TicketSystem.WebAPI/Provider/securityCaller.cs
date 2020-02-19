
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
            CommonService commonService = new CommonService();
            string plainEmailId = commonService.Decrypt(cipherEmailId);
            string encryptedPassword = commonService.Encrypt(Password);

            return _SecurityRepository.UpdatePassword(plainEmailId, encryptedPassword);
        }

        /// <summary>
        /// Send Mail 
        /// </summary>
        /// <param name="security">Interface </param>
        /// <param name="cipherEmailId">Encrypted email Id</param>
        /// <param name="Password">Plain text Password </param>
        /// <returns></returns>
        public bool sendMail(ISecurity security, SMTPDetails sMTPDetails, string EmailId, string content, int TenantId)
        {
            _SecurityRepository = security;
            CommonService commonService = new CommonService();


            return _SecurityRepository.sendMailForForgotPassword(sMTPDetails, EmailId, content, TenantId);
        }
        public bool sendMailForChangePassword(ISecurity security, SMTPDetails sMTPDetails, string EmailId, string content, int TenantId)
        {
            _SecurityRepository = security;
            CommonService commonService = new CommonService();


            return _SecurityRepository.sendMailForChangePassword(sMTPDetails, EmailId, content, TenantId);
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
        public AccountModal validateUser(ISecurity security, string ProgramCode, string Domainname, string userId, string password)
        {
            _SecurityRepository = security;
            return _SecurityRepository.AuthenticateUser(ProgramCode, Domainname, userId, password);
        }

        /// <summary>
        /// Logout user
        /// </summary>
        /// <param name="security"></param>
        /// <param name="token"></param>
        public void Logout(ISecurity security, string token)
        {
            _SecurityRepository = security;
            _SecurityRepository.Logout(token);
        }

        public Authenticate validateUserEmailId(ISecurity security, string EmailId)
        {
            _SecurityRepository = security;
            return _SecurityRepository.validateUserEmailId(EmailId);
        }

        /// <summary>
        /// validateProgramCode
        /// </summary>
        /// <param name="security"></param>
        /// <param name="Programcode"></param>
        /// <param name="Domainname"></param>
        /// <returns></returns>
        public bool validateProgramCode(ISecurity security, string Programcode, string Domainname)
        {
            _SecurityRepository = security;
            return _SecurityRepository.validateProgramCode(Programcode, Domainname);
        }
        #endregion
    }
}
