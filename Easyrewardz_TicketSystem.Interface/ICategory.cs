using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.CustomModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Category
    /// </summary>
    public partial interface ICategory
    {
        /// <summary>
        /// Get Category List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        List<Category> GetCategoryList(int TenantID, int BrandID);

        /// <summary>
        /// Get Category On Search
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="BrandID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        List<Category> GetCategoryOnSearch(int TenantID, int BrandID, string searchText);

        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="BrandID"></param>
        /// <returns></returns>
        int AddCategory(string category, int TenantID, int UserID, int BrandID);

        /// <summary>
        /// Category List
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<Category> CategoryList(int TenantId);

        /// <summary>
        /// Get Category List By Multi Brand ID
        /// </summary>
        /// <param name="BrandIDs"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<Category> GetCategoryListByMultiBrandID(string BrandIDs, int TenantId);

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int DeleteCategory(int CategoryID, int TenantId);

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        int UpdateCategory(Category category);

        /// <summary>
        /// Create Category Brand Mapping
        /// </summary>
        /// <param name="customCreateCategory"></param>
        /// <returns></returns>
        int CreateCategoryBrandMapping(CustomCreateCategory customCreateCategory);

        /// <summary>
        /// List Category Brand Mapping
        /// </summary>
        /// <returns></returns>
        List<CustomCreateCategory> ListCategoryBrandMapping();

        /// <summary>
        /// Bulk Upload Category
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CategoryFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> BulkUploadCategory(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV);

        /// <summary>
        /// Create Claim Category
        /// </summary>
        /// <param name="claimCategory"></param>
        /// <returns></returns>
        int CreateClaimCategory(ClaimCategory claimCategory);


    }
}
