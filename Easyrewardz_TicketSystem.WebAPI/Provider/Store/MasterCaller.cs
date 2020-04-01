using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class MasterCaller
    {
      
        #region Store Category

        public List<CustomCreateCategory> ClaimCategoryList(ICategory _category, int TenantID)
        {
            _categoryList = _category;
            return _categoryList.ClaimCategoryList(TenantID);
        }

        
        public List<Category> GetClaimCategoryList(ICategory _category, int TenantID, int BrandID)
        {
            _categoryList = _category;
            return _categoryList.GetClaimCategoryList(TenantID, BrandID);
        }


        public int AddClaimCategory(ICategory _category, string CategoryName, int BrandID, int TenantID, int UserID)
        {
            _categoryList = _category;
            return _categoryList.AddClaimCategory(CategoryName, BrandID, TenantID, UserID);
        }

        public List<SubCategory> GetClaimSubCategoryByCategoryID(ICategory _category, int CategoryID, int TypeId)
        {
            _categoryList = _category;
            return _categoryList.GetClaimSubCategoryByCategoryID(CategoryID, TypeId);
        }

        public int AddClaimSubCategory(ICategory _category, int CategoryID, string category, int TenantID, int UserID)
        {
            _categoryList = _category;
            return _categoryList.AddClaimSubCategory(CategoryID, category, TenantID, UserID);
        }

        public List<IssueType> GetClaimIssueTypeList(ICategory _category, int TenantID, int SubCategoryID)
        {
            _categoryList = _category;
            return _categoryList.GetClaimIssueTypeList(TenantID, SubCategoryID);
        }
        public int AddClaimIssueType(ICategory _category, int SubcategoryID, string IssuetypeName, int TenantID, int UserID)
        {
            _categoryList = _category;
            return _categoryList.AddClaimIssueType(SubcategoryID, IssuetypeName, TenantID, UserID);
        }

        public int CreateClaimCategorybrandmapping(ICategory _category, CustomCreateCategory customCreateCategory)
        {
            _categoryList = _category;
            return _categoryList.CreateClaimCategorybrandmapping(customCreateCategory);
        }

        public int DeleteClaimCategory(ICategory _category, int CategoryID, int TenantId)
        {
            _categoryList = _category;
            return _categoryList.DeleteClaimCategory(CategoryID, TenantId);
        }
        #endregion

      }
}
