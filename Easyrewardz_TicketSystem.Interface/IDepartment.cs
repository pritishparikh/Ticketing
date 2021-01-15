using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IDepartment
    {

        /// <summary>
        /// Delete Department Mapping
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="DepartmentBrandMappingID"></param>
        /// <returns></returns>
        int DeleteDepartmentMapping(int tenantID, int DepartmentBrandMappingID);

        /// <summary>
        /// Update Department Mapping
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="DepartmentBrandID"></param>
        /// <param name="BrandID"></param>
        /// <param name="StoreID"></param>
        /// <param name="DepartmentID"></param>
        /// <param name="FunctionID"></param>
        /// <param name="Status"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        int UpdateDepartmentMapping(int TenantID, int DepartmentBrandID, int BrandID, int StoreID, int DepartmentID, int FunctionID, bool Status, int CreatedBy);


    }
}
