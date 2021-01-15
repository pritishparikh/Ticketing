using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Category
    /// </summary>
    public partial interface ICategory
    {
        /// <summary>
        /// Claim Category List
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<CustomCreateCategory> ClaimCategoryList(int TenantId);

        /// <summary>
        /// Get Claim Category List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        List<Category> GetClaimCategoryList(int TenantID, int BrandID);

        /// <summary>
        /// Get Claim Category By Search
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        List<Category> GetClaimCategoryBySearch(int TenantID, string CategoryName);

        /// <summary>
        /// Add Claim Category
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <param name="BrandID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AddClaimCategory(string CategoryName, int BrandID, int TenantID, int UserID);

        /// <summary>
        /// Get Claim SubCategory By Category ID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        List<SubCategory> GetClaimSubCategoryByCategoryID(int CategoryID, int TypeId);

        /// <summary>
        /// Get Claim SubCategory By Category On Search
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="CategoryID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        List<SubCategory> GetClaimSubCategoryByCategoryOnSearch(int tenantID, int CategoryID, string searchText);

        /// <summary>
        /// Add Claim SubCategory
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="category"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AddClaimSubCategory(int CategoryID, string category, int TenantID, int UserID);

        /// <summary>
        /// Get Claim IssueType List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="SubCategoryID"></param>
        /// <returns></returns>
        List<IssueType> GetClaimIssueTypeList(int TenantID, int SubCategoryID);

        /// <summary>
        /// Get Claim IssueType On Search
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="SubCategoryID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        List<IssueType> GetClaimIssueTypeOnSearch(int TenantID, int SubCategoryID, string searchText);

        /// <summary>
        /// Add Claim IssueType
        /// </summary>
        /// <param name="SubcategoryID"></param>
        /// <param name="IssuetypeName"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AddClaimIssueType(int SubcategoryID, string IssuetypeName, int TenantID, int UserID);

        /// <summary>
        /// Create Claim Category brand mapping
        /// </summary>
        /// <param name="customCreateCategory"></param>
        /// <returns></returns>
        int CreateClaimCategorybrandmapping(CustomCreateCategory customCreateCategory);

        /// <summary>
        /// Delete Claim Category
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int DeleteClaimCategory(int CategoryID, int TenantId);

        /// <summary>
        /// Bulk Upload Claim Category
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CategoryFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> BulkUploadClaimCategory(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV);

    }
}
