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
        DataSet validateTokenGetPermission(string secertCode,int ModuleID);

        /// <summary>
        /// Update password
        /// </summary>
        /// <param name="cipherEmailId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        bool UpdatePassword(string cipherEmailId, string Password);

        /// <summary>
        /// Send mail for the forgot password
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        bool sendMailForForgotPassword(SMTPDetails sMTPDetails, string emailId,string content, int TenantId);

        /// <summary>
        /// Authenticate User 
        /// </summary>
        /// <param name="Program_Code"></param>
        /// <param name="Domain_Name"></param>
        /// <param name="User_EmailID"></param>
        /// <param name="User_Password"></param>
        /// <returns></returns>
        AccountModal AuthenticateUser(string Program_Code, string Domain_Name, string User_EmailID, string User_Password);

        /// <summary>
        /// Logout 
        /// </summary>
        /// <param name="token"></param>
        void Logout(string token);

        /// <summary>
        /// Email id
        /// </summary>
        /// <param name="EmailId"></param>
        /// <returns></returns>
        Authenticate validateUserEmailId(string EmailId);

        /// <summary>
        /// validateProgramCode
        /// </summary>
        /// <param name="Programcode"></param>
        /// <param name="Domainname"></param>
        /// <returns></returns>
        bool validateProgramCode(string Programcode, string Domainname);

        /// <summary>
        /// Send mail for the Change password
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        bool sendMailForChangePassword(SMTPDetails sMTPDetails, string emailId, string content, int TenantId);
    }
}
