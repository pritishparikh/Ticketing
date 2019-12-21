using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IIssueType
    {
        /// <summary>
        /// Inteface for the Issue Type
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="SubCategoryID"></param>
        /// <returns></returns>
        List<IssueType> GetIssueTypeList(int TenantID,int SubCategoryID);
    }
}
