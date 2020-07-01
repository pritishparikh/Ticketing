using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Brand
    /// </summary>
    public interface IBrand
    {
        /// <summary>
        /// Get Brand List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<Brand> GetBrandList(int TenantID);

        /// <summary>
        /// Add Brand
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int AddBrand(Brand brand, int TenantId);

        /// <summary>
        /// Brand List
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<Brand> BrandList(int TenantId);

        /// <summary>
        /// Delete Brand
        /// </summary>
        /// <param name="BrandID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int DeleteBrand(int BrandID, int TenantId);

        /// <summary>
        /// Update Brand
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        int UpdateBrand(Brand brand);
    }
}
