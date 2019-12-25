using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Security
    /// </summary>
    public interface ISecurity
    {
        AccountModal getToken(string programCode, string AppID, string Domainname,string userId,string password);

        DataSet validateTokenGetPermission(string secertCode,int ModuleID);

        bool UpdatePassword(string cipherEmailId, string Password);

        bool sendMailForForgotPassword(string emailId,string content);
    }
}
