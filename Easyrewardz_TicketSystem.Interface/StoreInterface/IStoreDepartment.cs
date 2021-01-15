using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
   public  interface IStoreDepartment
    {
        /// <summary>
        /// Get department list
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<List<StoreDepartmentModel>> GetStoreDepartmentList(int TenantID, int UserID);



        /// <summary>
        /// Get Department by search
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="DepartmentName"></param>
        /// <returns></returns>
        List<StoreDepartmentModel> GetStoreDepartmentBySearch(int TenantID, string DepartmentName);

        /// <summary>
        /// Get function by department
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<StoreFunctionModel> GetStoreFunctionByDepartment(int DepartmentID, int TenantID);


        /// <summary>
        /// Search function by department ID and Namne
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="SearchText"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<StoreFunctionModel> SearchStoreFunctionByDepartment(int DepartmentID, string SearchText, int TenantID);


        /// <summary>
        /// Get function by mutilpe department
        /// </summary>
        /// <param name="DepartmentIDs"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<StoreFunctionModel> GetStoreFunctionbyMultiDepartment(string DepartmentIds, int TenantID);

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
        /// <param name="updateDepartmentModel"></param>
        /// <returns></returns>
        int UpdateDepartmentMapping(CreateStoreDepartmentModel updateDepartmentModel);

        /// <summary>
        /// get Store By Brand ID
        /// </summary>
        /// <param name="BrandIDs"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<StoreCodeModel> getStoreByBrandID(string BrandIDs, int tenantID);

        /// <summary>
        /// Create Department
        /// </summary>
        /// <returns></returns>
        int CreateDepartment(CreateStoreDepartmentModel createDepartmentModel);

        /// <summary>
        /// Get Brand Department Mapping List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<DepartmentListingModel> GetBrandDepartmentMappingList(int TenantID);

        /// <summary>
        /// Department Bulk Upload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CategoryFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> DepartmentBulkUpload(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV);
    }
}
