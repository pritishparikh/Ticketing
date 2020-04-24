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
        List<Category> GetCategoryList(int TenantID, int BrandID);

        List<Category> GetCategoryOnSearch(int TenantID, int BrandID, string searchText);
        int AddCategory(string category, int TenantID, int UserID, int BrandID);

        List<Category> CategoryList(int TenantId);

        List<Category> GetCategoryListByMultiBrandID(string BrandIDs, int TenantId);

        int DeleteCategory(int CategoryID, int TenantId);

        int UpdateCategory(Category category);

        int CreateCategoryBrandMapping(CustomCreateCategory customCreateCategory);

        List<CustomCreateCategory> ListCategoryBrandMapping();

        List<string> BulkUploadCategory(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV);

        int CreateClaimCategory(ClaimCategory claimCategory);


    }
}
