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
        /// <summary>
        /// Add Issue Type
        /// </summary>
        /// <param name="SubcategoryID"></param>
        /// <param name="IssuetypeName"></param>
        /// <returns></returns>
        int AddIssueType(int SubcategoryID, string IssuetypeName ,int TenantID, int UserID);

        /// <summary>
        /// Inteface for the Issue Type
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="SubCategoryID"></param>
        /// <returns></returns>
        List<IssueType> GetIssueTypeListByMultiSubCategoryID(int TenantID, string SubCategoryIDs);
        /// <summary>
        /// Inteface for the Issue Type on search
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="SubCategoryID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        List<IssueType> GetIssueTypeOnSearch(int TenantID, int SubCategoryID, string searchText);
    }
}
