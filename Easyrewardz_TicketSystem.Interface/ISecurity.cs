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

        /// <summary>
        /// Authenticate User 
        /// </summary>
        /// <param name="Program_Code"></param>
        /// <param name="Domain_Name"></param>
        /// <param name="User_EmailID"></param>
        /// <param name="User_Password"></param>
        /// <returns></returns>
        AccountModal AuthenticateUser(string Program_Code, string Domain_Name, string User_EmailID, string User_Password);
    }
}
