using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class MasterCaller
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

        public List<Category> GetCategoryList(ICategory _category, int TenantID)
        {
            _categoryList = _category;
            return _categoryList.GetCategoryList(TenantID);
        }

        public int AddCategory(ICategory _category, Category category)
        {
            _categoryList = _category;
            return _categoryList.AddCategory(category);
        }

        public List<Category> CategoryList(ICategory _category, int TenantID)
        {
            _categoryList = _category;
            return _categoryList.CategoryList(TenantID);
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

        #endregion

        #region Methods for the Subcategories

        public List<SubCategory> GetSubCategoryByCategoryID(ISubCategories _SubCategory, int CategoryID)
        {
            _subCategoryList = _SubCategory;
            return _subCategoryList.GetSubCategoryByCategoryID(CategoryID);
        }

        #endregion

        #region Methods for the Priority

        public List<Priority> GetPriorityList(IPriority _priority, int TenantID)
        {
            _priorityList = _priority;
            return _priorityList.GetPriorityList(TenantID);
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
        #endregion


        #region Methods for the User
        public List<User> GetUserList(IUser _user, int TenantID,int UserID)
        {
            _userlist = _user;
            return _userlist.GetUserList(TenantID, UserID);
        }
        #endregion

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

        #region PaymentMode

        public List<PaymentMode> GetPaymentMode(IMasterInterface _ImasterInterface)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetPaymentMode();
        }

        #endregion

        #region PaymentMode

        public List<TicketSourceMaster> GetTicketSource(IMasterInterface _ImasterInterface)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetTicketSources();
        }

        #endregion

        public SMTPDetails GetSMTPDetails(IMasterInterface _ImasterInterface,int TenantId)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetSMTPDetails(TenantId);
        }

        public List <StateMaster> GetStatelist(IMasterInterface _ImasterInterface)
        {
            _Imaster = _ImasterInterface;
            return _Imaster.GetStateList();
        }
    }
}
