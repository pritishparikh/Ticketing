using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Services;
using System.Data;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreSecurityCaller
    {
        #region Variable Declaration
        private IStoreSecurity _SecurityRepository;
        #endregion

        #region Custom Methods

        /// <summary>
        /// Validate token and get permission
        /// </summary>
        /// <param name="security"></param>
        /// <param name="SecretToken"></param>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public DataSet validateTokenGetPermission(IStoreSecurity security, string SecretToken, int ModuleID)
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
        public bool UpdatePassword(IStoreSecurity security, string cipherEmailId, string Password)
        {
            _SecurityRepository = security;
            CommonService commonService = new CommonService();
            //string plainEmailId = string.Empty;
            //try
            //{
            //    plainEmailId = commonService.Decrypt(cipherEmailId);
            //}
            //catch 
            //{
            //    plainEmailId = cipherEmailId;
            //}

            //string encryptedPassword = commonService.Encrypt(Password);

            return _SecurityRepository.UpdatePassword(cipherEmailId, Password);
        }

        /// <summary>
        /// Send Mail 
        /// </summary>
        /// <param name="security">Interface </param>
        /// <param name="cipherEmailId">Encrypted email Id</param>
        /// <param name="Password">Plain text Password </param>
        /// <returns></returns>
        public bool sendMail(IStoreSecurity security, SMTPDetails sMTPDetails, string EmailId, string subject, string content, int TenantId)
        {
            _SecurityRepository = security;
            CommonService commonService = new CommonService();


            return _SecurityRepository.sendMailForForgotPassword(sMTPDetails, EmailId, subject, content, TenantId);
        }
        public bool sendMailForChangePassword(IStoreSecurity security, SMTPDetails sMTPDetails, string EmailId, string content, int TenantId)
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
        public AccountModal validateUser(IStoreSecurity security, string ProgramCode, string Domainname, string userId, string password)
        {
            _SecurityRepository = security;
            return _SecurityRepository.AuthenticateUser(ProgramCode, Domainname, userId, password);
        }


        /// <summary>
        /// Validate User Account
        /// </summary>
        /// <param name="security"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="Domainname"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="deviceSource"></param>
        /// <param name="FBId"></param>
        /// <param name="deviceID"></param>
        /// <returns></returns>
        public AccountModal validateUser(IStoreSecurity security, string ProgramCode, string Domainname, string userId, string password, string deviceSource, string FBId, string deviceID)
        {
            _SecurityRepository = security;
            return _SecurityRepository.AuthenticateUser(ProgramCode, Domainname, userId, password, deviceSource, FBId, deviceID);
        }


        /// <summary>
        /// Update Mobile Details 
        /// </summary>     
        /// <param name="ProgramCode"></param>
        /// <param name="FBID"></param>
        /// <param name="DeviceID"></param>
        /// <returns></returns>
        public async Task<int> UpdateMobileDetails(IStoreSecurity security, int TenantID, string FBID, string DeviceID)
        {
            _SecurityRepository = security;
            return await _SecurityRepository.UpdateMobileDetails(TenantID, FBID, DeviceID);
        }
        /// <summary>
        /// Logout user
        /// </summary>
        /// <param name="security"></param>
        /// <param name="token"></param>
        public void Logout(IStoreSecurity security, string token)
        {
            _SecurityRepository = security;
            _SecurityRepository.Logout(token);
        }

        public Authenticate validateUserEmailId(IStoreSecurity security, string EmailId)
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
        public bool validateProgramCode(IStoreSecurity security, string Programcode, string Domainname)
        {
            _SecurityRepository = security;
            return _SecurityRepository.validateProgramCode(Programcode, Domainname);
        }
        public bool ChangePassword(IStoreSecurity security, CustomChangePassword customChangePassword, int TenantId, int UserID)
        {
            _SecurityRepository = security;
            return _SecurityRepository.ChangePassword(customChangePassword, TenantId, UserID);
        }

        public void GetForgetPassowrdMailContent(IStoreSecurity security, int TenantId, string url, string emailid, out string content, out string subject)
        {
            _SecurityRepository = security;
            _SecurityRepository.GetForgetPassowrdMailContent(TenantId, url, emailid, out content, out subject);
        }
        #endregion
    }
}
