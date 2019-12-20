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
        #endregion

        #region Methods


        public List<Brand> GetBrandList(IBrand _brand, int TenantID)
        {
            _brandList = _brand;
            return _brandList.GetBrandList(TenantID);
        }


        public List<Category> GetCategoryList(ICategory _category, int TenantID)
        {
            _categoryList = _category;
            return _categoryList.GetCategoryList(TenantID);
        }



        public List<SubCategory> GetSubCategoryByCategoryID(ISubCategories _SubCategory, int CategoryID)
        {
            _subCategoryList = _SubCategory;
            return _subCategoryList.GetSubCategoryByCategoryID(CategoryID);
        }


        public List<Priority> GetPriorityList(IPriority _priority, int TenantID)
        {
            _priorityList = _priority;
            return _priorityList.GetPriorityList(TenantID);
        }

        public List<ChannelOfPurchase> GetChannelOfPurchaseList(IMasterInterface _IChannel, int TenantID)
        {
            _ImasterChannel = _IChannel;
            return _ImasterChannel.GetChannelOfPurchaseList(TenantID);
        }

        public List<IssueType> GetIssueTypeList(IIssueType _issue, int TenantID, int SubCategoryID)
        {
            _issueList = _issue;
            return _issueList.GetIssueTypeList(TenantID, SubCategoryID);
        }


        #endregion

    }
}
