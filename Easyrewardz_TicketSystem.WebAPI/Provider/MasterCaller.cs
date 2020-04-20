using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class MasterCaller
    {
        #region Variable

        /// <summary>
        /// Brand
        /// </summary>
        private IBrand _brandList;

        /// <summary>
        /// Cateogry
        /// </summary>
        private ICategory _categoryList;

        /// <summary>
        /// Subcategory
        /// </summary>
        private ISubCategories _subCategoryList;

        /// <summary>
        /// Priority
        /// </summary>
        private IPriority _priorityList;

        /// <summary>
        /// IssueType
        /// </summary>
        private IIssueType _issueList;

        /// <summary>
        /// ChannelOfPurchase
        /// </summary>
        private IMasterInterface _ImasterChannel;

        private IMasterInterface _Imaster;

        private IUser _userlist;


        #endregion

        #region Methods for the Brand

        public List<Brand> GetBrandList(IBrand _brand, int TenantID)
        {
            _brandList = _brand;
            return _brandList.GetBrandList(TenantID);
        }

        public int AddBrand(IBrand _brand, Brand brand, int TenantId)
        {
            _brandList = _brand;
            return _brandList.AddBrand(brand, TenantId);
        }

            public List<Brand> BrandList(IBrand _brand, int TenantId)
        {
            _brandList = _brand;
            return _brandList.BrandList(TenantId);
        }

        public int DeleteBrand(IBrand _brand, int BrandID, int TenantId)
        {
            _brandList = _brand;
            return _brandList.DeleteBrand(BrandID, TenantId);
        }

        public int UpdateBrand(IBrand _brand, Brand brand)
        {
            _brandList = _brand;
            return _brandList.UpdateBrand(brand);
        }

        #endregion

        #region Methods for the Category

        public List<Category> GetCategoryList(ICategory _category, int TenantID,int BrandID)
        {
            _categoryList = _category;
            return _categoryList.GetCategoryList(TenantID, BrandID);
        }
        public List<Category> GetCategoryOnSearch(ICategory _category, int TenantID, int BrandID, string searchText)
        {
            _categoryList = _category;
            return _categoryList.GetCategoryOnSearch(TenantID, BrandID, searchText);
        }
        public int AddCategory(ICategory _category, string category, int TenantID, int UserID, int BrandID)
        {
            _categoryList = _category;
            return _categoryList.AddCategory(category, TenantID, UserID,BrandID);
        }

        public List<Category> CategoryList(ICategory _category, int TenantID)
        {
            _categoryList = _category;
            return _categoryList.CategoryList(TenantID);
        }
        public List<Category> GetCategoryListByMultiBrandID(ICategory _category, string BrandIDs , int TenantID)
        {
            _categoryList = _category;
            return _categoryList.GetCategoryListByMultiBrandID(BrandIDs,TenantID);
        }

        public int DeleteCategory(ICategory _category, int CategoryID, int TenantId)
        {
            _categoryList = _category;
            return _categoryList.DeleteCategory(CategoryID, TenantId);
        }

        public int UpdateCategory(ICategory _category, Category category)
        {
            _categoryList = _category;
            return _categoryList.UpdateCategory(category);
        }
        public int CreateCategoryBrandMapping(ICategory _category, CustomCreateCategory customCreateCategory)
        {
            _categoryList = _category;
            return _categoryList.CreateCategoryBrandMapping(customCreateCategory);
        }
        public List<CustomCreateCategory> ListCategoryBrandMapping(ICategory _category)
        {
            _categoryList = _category;
            return _categoryList.ListCategoryBrandMapping();
        }

        public List<string> CategoryBulkUpload(ICategory Category, int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV)
        {
            _categoryList = Category;
            return _categoryList.BulkUploadCategory(TenantID, CreatedBy, CategoryFor, DataSetCSV);
        }

        #endregion

        #region Methods for the Subcategories

        public List<SubCategory> GetSubCategoryByCategoryID(ISubCategories _SubCategory, int CategoryID,int TypeId)
        {
            _subCategoryList = _SubCategory;
            return _subCategoryList.GetSubCategoryByCategoryID(CategoryID,TypeId);
        }
        public List<SubCategory> GetSubCategoryByCategoryOnSearch(ISubCategories _SubCategory, int tenantID, int CategoryID, string searchText)
        {
            _subCategoryList = _SubCategory;
            return _subCategoryList.GetSubCategoryByCategoryOnSearch(tenantID, CategoryID, searchText);
        }
        public List<SubCategory> GetSubCategoryByMultiCategoryID(ISubCategories _SubCategory, string CategoryIDs)
        {
            _subCategoryList = _SubCategory;
            return _subCategoryList.GetSubCategoryByMultiCategoryID(CategoryIDs);
        }

        public int AddSubCategory(ISubCategories _SubCategory, int CategoryID, string category, int TenantID, int UserID)
        {
            _subCategoryList = _SubCategory;
            return _subCategoryList.AddSubCategory(CategoryID,category, TenantID, UserID);
        }


        #endregion

        #region Methods for the Priority

        public List<Priority> GetPriorityList(IPriority _priority, int TenantID,int PriorityFor)
        {
            _priorityList = _priority;
            return _priorityList.GetPriorityList(TenantID, PriorityFor);
        }
        public int  Addpriority(IPriority _priority, string PriorityName, int status, int tenantID, int UserID,int PriorityFor)
        {
            _priorityList = _priority;
            return _priorityList.AddPriority(PriorityName, status, tenantID, UserID, PriorityFor);
        }
        public int Updatepriority(IPriority _priority, int PriorityID, string PriorityName, int status, int tenantID, int UserID,int PriorityFor)
        {
            _priorityList = _priority;
            return _priorityList.UpdatePriority(PriorityID,PriorityName, status, tenantID, UserID, PriorityFor);
        }
        public int Deletepriority(IPriority _priority, int PriorityID, int tenantID, int UserID,int PriorityFor)
        {
            _priorityList = _priority;
            return _priorityList.DeletePriority(PriorityID,tenantID, UserID, PriorityFor);
        }
        public List<Priority> PriorityList(IPriority _priority, int TenantID, int PriorityFor)
        {
            _priorityList = _priority;
            return _priorityList.PriorityList(TenantID, PriorityFor);
        }
        public bool UpdatePriorityOrder(IPriority _priority, int TenantID, int selectedPriorityID, int currentPriorityID, int PriorityFor)
        {
            _priorityList = _priority;
            return _priorityList.UpdatePriorityOrder(TenantID, selectedPriorityID, currentPriorityID, PriorityFor);
        }

        #endregion

        #region Methods for the Channel of the Purchase
        public List<ChannelOfPurchase> GetChannelOfPurchaseList(IMasterInterface _IChannel, int TenantID)
        {
            _ImasterChannel = _IChannel;
            return _ImasterChannel.GetChannelOfPurchaseList(TenantID);
        }
        #endregion

        #region Methods for the Issue type list
        public List<IssueType> GetIssueTypeList(IIssueType _issue, int TenantID, int SubCategoryID)
        {
            _issueList = _issue;
            return _issueList.GetIssueTypeList(TenantID, SubCategoryID);
        }
        public List<IssueType> IssueTypeListByMultiSubCategoryID(IIssueType _issue, int TenantID, string SubCategoryIDs)
        {
            _issueList = _issue;
            return _issueList.GetIssueTypeListByMultiSubCategoryID(TenantID, SubCategoryIDs);
        }
        public int AddIssueType(IIssueType _issue, int SubcategoryID, string IssuetypeName, int TenantID, int UserID)
        {
            _issueList = _issue;
            return _issueList.AddIssueType(SubcategoryID, IssuetypeName, TenantID, UserID);
        }
        #endregion

        #region Methods for the User
        public List<User> GetUserList(IUser _user, int TenantID,int UserID)
        {
            _userlist = _user;
            return _userlist.GetUserList(TenantID, UserID);
        }
        #endregion

        #region Department

        private IMasterInterface _ImasterDepartment;
        public List<DepartmentMaster> GetDepartmentListDetails(IMasterInterface _department, int TenantID)
        {
            _ImasterDepartment = _department;
            return _ImasterDepartment.GetDepartmentList(TenantID);
        }

        private IMasterInterface _ImasterFunctionbyDepartment;
        public List<FuncationMaster> GetFunctionbyDepartment(IMasterInterface _function, int DepartmentID, int TenantID)
        {
            _ImasterFunctionbyDepartment = _function;
            return _ImasterFunctionbyDepartment.GetFunctionByDepartment(DepartmentID, TenantID);
        }

        private IMasterInterface _ImasterAddDepartment;
        public int AddDepartment(IMasterInterface _AddDepartment, string DepartmentName, int TenantID, int CreatedBy)
        {
            _ImasterAddDepartment = _AddDepartment;
            return _ImasterAddDepartment.AddDepartment(DepartmentName, TenantID, CreatedBy);
        }

        private IMasterInterface _ImasterAddFunction;
        public int AddFunction(IMasterInterface _AddFunction,int DepartmentID ,string FunctionName, int TenantID, int CreatedBy)
        {
            _ImasterAddFunction = _AddFunction;
            return _ImasterAddFunction.Addfunction(DepartmentID, FunctionName,TenantID, CreatedBy);
        }
        #endregion

        #region PaymentMode

        public List<PaymentMode> GetPaymentMode(IMasterInterface _ImasterInterface)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetPaymentMode();
        }

        #endregion

        #region Ticket Sources

        public List<TicketSourceMaster> GetTicketSource(IMasterInterface _ImasterInterface)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetTicketSources();
        }

        #endregion

        #region SMTP Details 

        public SMTPDetails GetSMTPDetails(IMasterInterface _ImasterInterface, int TenantId)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetSMTPDetails(TenantId);
        }


        #endregion

        #region State

        public List <StateMaster> GetStatelist(IMasterInterface _ImasterInterface)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetStateList();
        }

        #endregion

        #region City

        public List<CityMaster> GetCitylist(IMasterInterface _ImasterInterface,int StateId)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetCitylist(StateId);
        }

        #endregion

        #region Region

        public List<RegionMaster> GetRegionlist(IMasterInterface _ImasterInterface)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetRegionList();
        }

        #endregion

        #region Store

        public List<StoreTypeMaster> GetStoreTypelist(IMasterInterface _ImasterInterface)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetStoreTypeList();
        }
        public List<StoreTypeMaster> GetStoreNameWithStoreCode(IMasterInterface _ImasterInterface,int TenantID)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetStoreNameWithsStoreCode(TenantID);
        }

        #endregion

        #region Language
        public List<LanguageModel> GetLanguageList(IMasterInterface _masterInterface, int TenantID)
        {
            _Imaster = _masterInterface;
            return _Imaster.GetLanguageList(TenantID);
        }

        #endregion

        #region getCityStateCountry
        public List<CommonModel> GetCountryStateCityList(IMasterInterface _masterInterface,int TenantID,string Pincode)
        {
            _Imaster = _masterInterface;
            return _Imaster.GetCountryStateCityList(TenantID,Pincode);
        }

        #endregion

        #region create Department

        public int CreateDepartment(IMasterInterface _masterInterface, CreateDepartmentModel createDepartmentModel)
        {
            _Imaster = _masterInterface;
            return _Imaster.CreateDepartment(createDepartmentModel);
        }
        #endregion

        #region create Claim Categoty

        public int CreateClaimCategory(ICategory _category, ClaimCategory claimCategory)
        {
            _categoryList = _category;
            return _categoryList.CreateClaimCategory(claimCategory);
        }
        #endregion

        #region Get LogedInEmail

        public CustomGetEmailID GetLogedInEmail(IMasterInterface _masterInterface, int UserID,int TenantID)
        {
            _Imaster = _masterInterface;
            return _Imaster.GetLogedInEmail(UserID, TenantID);
        }
        #endregion
    }
}
