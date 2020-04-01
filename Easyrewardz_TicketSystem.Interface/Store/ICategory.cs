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
        
        List<CustomCreateCategory> ClaimCategoryList(int TenantId);
        
        List<Category> GetClaimCategoryList(int TenantID, int BrandID);

        int AddClaimCategory(string CategoryName, int BrandID, int TenantID, int UserID);

        int CreateClaimCategorybrandmapping(CustomCreateCategory customCreateCategory);

        int DeleteClaimCategory(int CategoryID, int TenantId);

    }
}
