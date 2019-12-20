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
        /// <summary>
        /// Get Brand list for Drop down 
        /// </summary>
        private IBrand _brandList;
        public List<Brand> GetBrandList(IBrand _brand,int TenantID)
        {
            _brandList = _brand;
            return _brandList.GetBrandList(TenantID);
        }

        /// <summary>
        /// Get Category list for Drop down 
        /// </summary>

        private ICategory _categoryList;

        public List<Category> GetCategoryList(ICategory _category, int TenantID)
        {
            _categoryList = _category;
            return _categoryList.GetCategoryList(TenantID);
        }


        /// <summary>
        /// Get SubCategory List Drop down 
        /// </summary>
        private ISubCategories _subCategoryList;
        public List<SubCategory> GetSubCategoryByCategoryID(ISubCategories _SubCategory, int CategoryID)
        {
            _subCategoryList = _SubCategory;
            return _subCategoryList.GetSubCategoryByCategoryID(CategoryID);
        }

        /// <summary>
        /// Get Priority List Drop down
        /// </summary>
        private IPriority _priorityList;
        public List<Priority> GetPriorityList(IPriority _priority, int TenantID)
        {
            _priorityList = _priority;
            return _priorityList.GetPriorityList(TenantID);
        }

        /// <summary>
        /// Get IssueType List Drop down
        /// </summary>
        private IIssueType _issueList;
        public List<IssueType> GetIssueTypeList(IIssueType _issue, int TenantID)
        {
            _issueList = _issue;
            return _issueList.GetIssueTypeList(TenantID);
        }

        /// <summary>
        /// Get ChannelOfPurchase List Drop down
        /// </summary>
        private IMasterInterface _ImasterChannel;
        public List<ChannelOfPurchase> GetChannelOfPurchaseList(IMasterInterface _IChannel, int TenantID)
        {
            _ImasterChannel = _IChannel;
            return _ImasterChannel.GetChannelOfPurchaseList(TenantID);
        }
    }
}
