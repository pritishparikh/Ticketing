using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class MasterCaller
    {
        private IItem _IItem;
        private IStoreDepartment _IStoreDepartment;
        private IMaster _IMaster;

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

        public List<Category> GetClaimCategoryBySearch(ICategory _category, int TenantID, string CategoryName)
        {
            _categoryList = _category;
            return _categoryList.GetClaimCategoryBySearch(TenantID, CategoryName);
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

        public List<SubCategory> GetClaimSubCategoryByCategoryOnSearch(ICategory _category, int tenantID, int CategoryID, string searchText)
        {
            _categoryList = _category;
            return _categoryList.GetClaimSubCategoryByCategoryOnSearch(tenantID, CategoryID, searchText);
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

        public List<IssueType> GetClaimIssueTypeOnSearch(ICategory _category, int TenantID, int SubCategoryID, string searchText)
        {
            _categoryList = _category;
            return _categoryList.GetClaimIssueTypeOnSearch(TenantID, SubCategoryID, searchText);
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

        public List<string> ClaimCategoryBulkUpload(ICategory Category, int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV)
        {
            _categoryList = Category;
            return _categoryList.BulkUploadClaimCategory(TenantID, CreatedBy, CategoryFor, DataSetCSV);
        }

        #endregion

        #region Store Item 

        public List<string> ItemBulkUpload(IItem Item, int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV)
        {
            _IItem = Item;
            return _IItem.ItemBulkUpload(TenantID, CreatedBy, CategoryFor, DataSetCSV);
        }

        public List<ItemModel> GetItemList(IItem Item, int TenantID)
        {
            _IItem = Item;
            return _IItem.GetItemList(TenantID);
        }
        #endregion


        public async Task<List<StoreDepartmentModel>> GetStoreDepartmentList(IStoreDepartment _department, int TenantID,int UserID)
        {
            _IStoreDepartment = _department;
            return await _IStoreDepartment.GetStoreDepartmentList(TenantID, UserID);
        }

        #region Methods for Store User
        public List<StoreUser> GetStoreUserList(IMaster _user, int TenantID, int UserID)
        {
            _IMaster = _user;
            return _IMaster.GetStoreUserList(TenantID, UserID);
        }
        #endregion

        #region Methods for the User
        public List<StoreFunctionModel> GetStoreFunctionList(IMaster _user, int TenantID, int UserID)
        {
            _IMaster = _user;
            return _IMaster.GetStoreFunctionList(TenantID, UserID);
        }
        #endregion

        public List<RegionZoneMaster> GetRegionlist(IMaster _ImasterInterface, int UserID)
        {
            _IMaster = _ImasterInterface;
            return _IMaster.GetRegionlist(UserID);
        }

    }
}
