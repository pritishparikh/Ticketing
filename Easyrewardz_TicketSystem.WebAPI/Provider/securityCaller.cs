
using Easyrewardz_TicketSystem.Interface;
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
    }
}
