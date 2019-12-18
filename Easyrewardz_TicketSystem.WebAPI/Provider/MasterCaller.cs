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
    }
}
