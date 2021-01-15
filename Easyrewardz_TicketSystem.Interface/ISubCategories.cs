using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Category
    /// </summary>
    public interface ISubCategories
    {
        /// <summary>
        /// Get Sub Category By Category ID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        List<SubCategory> GetSubCategoryByCategoryID(int CategoryID,int TypeId);

        /// <summary>
        /// Get Sub Category By Multi Category ID
        /// </summary>
        /// <param name="CategoryIDs"></param>
        /// <returns></returns>
        List<SubCategory> GetSubCategoryByMultiCategoryID(string CategoryIDs);

        /// <summary>
        /// Add Sub Category
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="category"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int AddSubCategory(int CategoryID,string category, int TenantID, int UserID);

        /// <summary>
        /// Get Sub Category By Category On Search
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="CategoryID"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        List<SubCategory> GetSubCategoryByCategoryOnSearch(int tenantID,int CategoryID, string searchText);
    }
}
