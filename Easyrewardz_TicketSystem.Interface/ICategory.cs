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
    public interface ICategory
    {
        List<Category> GetCategoryList(int TenantID,int BrandID);

        int AddCategory(string category,int TenantID,int UserID, int BrandID);

        List<Category> CategoryList(int TenantId);

        List<Category> GetCategoryListByMultiBrandID(string BrandIDs, int TenantId);

        int DeleteCategory(int CategoryID, int TenantId);

        int UpdateCategory(Category category);

        int CreateCategoryBrandMapping(CustomCreateCategory customCreateCategory);

        List<CustomCreateCategory> ListCategoryBrandMapping();

        List<string> BulkUploadCategory(int TenantID, int CreatedBy,int CategoryFor, DataSet DataSetCSV);

        int CreateClaimCategory(ClaimCategory claimCategory);

        #region Store Category

        List<Category> ClaimCategoryList(int TenantId);

        List<Category> GetClaimCategoryList(int TenantID, int BrandID);

        int AddClaimCategory(string CategoryName, int BrandID, int TenantID, int UserID);

        #endregion
    }
}
