using Easyrewardz_TicketSystem.Interface.StoreInterface;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreDepartmentCaller
    {

        #region Department

        private IStoreDepartment ImasterDepartment;

        public List<StoreDepartmentModel> GetDepartmentListDetails(IStoreDepartment department, int TenantID)
        {
            ImasterDepartment = department;
            return ImasterDepartment.GetStoreDepartmentList(TenantID);
        }

      
        public List<StoreFunctionModel> GetStoreFunctionbyDepartment(IStoreDepartment function, int DepartmentID, int TenantID)
        {
            ImasterDepartment = function;
            return ImasterDepartment.GetStoreFunctionByDepartment(DepartmentID, TenantID);
        }

        public List<StoreFunctionModel> GetStoreFunctionbyMultiDepartment(IStoreDepartment function, string DepartmentIds, int TenantID)
        {
            ImasterDepartment = function;
            return ImasterDepartment.GetStoreFunctionbyMultiDepartment(DepartmentIds, TenantID); 
        }


        public int AddDepartment(IStoreDepartment AddDepartment, string DepartmentName, int TenantID, int CreatedBy)
        {
            ImasterDepartment = AddDepartment;
            return ImasterDepartment.AddStoreDepartment(DepartmentName, TenantID, CreatedBy);
        }

       
        public int AddFunction(IStoreDepartment AddFunction, int DepartmentID, string FunctionName, int TenantID, int CreatedBy)
        {
            ImasterDepartment = AddFunction;
            return ImasterDepartment.AddStorefunction(DepartmentID, FunctionName, TenantID, CreatedBy);
        }


        public int DeleteDepartmentMapping(IStoreDepartment dept, int tenantID, int DepartmentBrandMappingID)
        {
            ImasterDepartment = dept;
            return ImasterDepartment.DeleteDepartmentMapping(tenantID, DepartmentBrandMappingID);
        }


        public int UpdateDepartmentMapping(IStoreDepartment dept, CreateStoreDepartmentModel updateDepartmentModel)
        {
            ImasterDepartment = dept;
            return ImasterDepartment.UpdateDepartmentMapping(updateDepartmentModel);
        }

        public List<StoreCodeModel> getStoreByBrandID(IStoreDepartment Store, string BrandIDs, int tenantID)
        {
            ImasterDepartment = Store;
            return ImasterDepartment.getStoreByBrandID(BrandIDs, tenantID);
        }

        public int CreateStoreDepartment(IStoreDepartment Store, CreateStoreDepartmentModel createDepartmentModel)
        {
            ImasterDepartment = Store;
            return ImasterDepartment.CreateDepartment(createDepartmentModel);
        }

        public List<DepartmentListingModel> GetBrandDepartmenMappingtList(IStoreDepartment dept, int TenantID)
        {
            ImasterDepartment = dept;
            return ImasterDepartment.GetBrandDepartmentMappingList(TenantID);
        }

        public List<string> DepartmentBulkUpload(IStoreDepartment dept, int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV)
        {
            ImasterDepartment = dept;
            return ImasterDepartment.DepartmentBulkUpload(TenantID, CreatedBy, CategoryFor, DataSetCSV);
        }
        #endregion
    }
}
