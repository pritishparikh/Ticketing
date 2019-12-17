using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ISecurity
    {
        string getToken(string programCode, string AppID, string Domainname,string userId,string password);

        DataSet validateTokenGetPermission(string secertCode,int ModuleID);

        bool UpdatePassword(string cipherEmailId, string Password);
    }
}
