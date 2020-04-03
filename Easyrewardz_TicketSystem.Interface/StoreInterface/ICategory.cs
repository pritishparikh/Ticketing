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
        
        List<CustomCreateCategory> ClaimCategoryList(int TenantId);
        
        List<Category> GetClaimCategoryList(int TenantID, int BrandID);

        int AddClaimCategory(string CategoryName, int BrandID, int TenantID, int UserID);

        List<SubCategory> GetClaimSubCategoryByCategoryID(int CategoryID, int TypeId);

        int AddClaimSubCategory(int CategoryID, string category, int TenantID, int UserID);

        List<IssueType> GetClaimIssueTypeList(int TenantID, int SubCategoryID);

        int AddClaimIssueType(int SubcategoryID, string IssuetypeName, int TenantID, int UserID);

        int CreateClaimCategorybrandmapping(CustomCreateCategory customCreateCategory);

        int DeleteClaimCategory(int CategoryID, int TenantId);

        List<string> BulkUploadClaimCategory(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV);

    }
}
