
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Services;
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class securityCaller
    {
        private ISecurity _SecurityRepository;
        public string generateToken(ISecurity security, string ProgramCode, string Domainname, string applicationid, string userId, string password)
        {
            _SecurityRepository = security;
            return _SecurityRepository.getToken(ProgramCode, applicationid, Domainname, userId, password);
        }

        public DataSet validateTokenGetPermission(ISecurity security, string SecretToken, int ModuleID)
        {
            _SecurityRepository = security;
            return _SecurityRepository.validateTokenGetPermission(SecretToken,ModuleID);
        }
        public bool UpdatePassword(ISecurity security,string cipherEmailId,string Password)
        {
            CommonService objSmdService = new CommonService();
            string plainEmailId = objSmdService.Decrypt(cipherEmailId);
            string encryptedPassword = objSmdService.Encrypt(Password);

            return _SecurityRepository.UpdatePassword(plainEmailId, encryptedPassword);
        }
    }
}
