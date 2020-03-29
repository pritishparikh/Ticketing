using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Category
    /// </summary>
    public interface ISubCategories
    {
        List<SubCategory> GetSubCategoryByCategoryID(int CategoryID,int TypeId);
        List<SubCategory> GetSubCategoryByMultiCategoryID(string CategoryIDs);
        int AddSubCategory(int CategoryID,string category, int TenantID, int UserID);
    }
}
