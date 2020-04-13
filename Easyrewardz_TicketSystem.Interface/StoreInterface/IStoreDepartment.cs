using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
   public  interface IStoreDepartment
    {
        /// <summary>
        /// Get department list
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<StoreDepartmentModel> GetStoreDepartmentList(int TenantID);

        /// <summary>
        /// Get function by department
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<StoreFunctionModel> GetStoreFunctionByDepartment(int DepartmentID, int TenantID);

        /// <summary>
        /// Add Department
        /// </summary>
        /// <param name="DepartmentName"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        int AddStoreDepartment(string DepartmentName, int TenantID, int CreatedBy);

        /// <summary>
        /// Add Function
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        int AddStorefunction(int DepartmentID, string FunctionName, int TenantID, int CreatedBy);

        int DeleteDepartmentMapping(int tenantID, int DepartmentBrandMappingID);

        int UpdateDepartmentMapping(CreateStoreDepartmentModel updateDepartmentModel);

        List<StoreCodeModel> getStoreByBrandID(string BrandIDs, int tenantID);

        /// <summary>
        /// Create Department
        /// </summary>
        /// <returns></returns>
        int CreateDepartment(CreateStoreDepartmentModel createDepartmentModel);


        List<DepartmentListingModel> GetBrandDepartmentMappingList(int TenantID);

        List<string> DepartmentBulkUpload(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV);
    }
}
