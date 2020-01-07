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
        List<Brand> GetBrandList(int TenantID);

        int AddBrand(Brand brand, int TenantId);

        List<Brand> BrandList(int TenantId);

        int DeleteBrand(int BrandID, int TenantId);

        int UpdateBrand(Brand brand);
    }
}
