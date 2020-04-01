using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class DepartmentCaller
    {
        #region Variable
        public IDepartment _deptRepository;
        #endregion


        //public List<DepartmentDetailsModel> GetDepartmentDetails(IDepartment dept, int tenantID)
        //{
        //    _deptRepository = dept;
        //    return _deptRepository.GetDepartmentDetails(tenantID);
        //}

        //public List<FunctionModel> GetFunctionDetailsByDepartmentID(IDepartment dept, int tenantID, int departmentID)
        //{
        //    _deptRepository = dept;
        //      return _deptRepository.GetFunctionDetailsByDepartmentID( tenantID, departmentID);
        //}
        //public int CreateDepartment(IDepartment dept, int tenantID, string departmentName, bool isDepartmentActive)
        //{
        //    _deptRepository = dept;
        //    return _deptRepository.CreateDepartment( tenantID,  departmentName,  isDepartmentActive);
        //}

        //public int CreateFunction(IDepartment dept, int tenantID, int departmentID, string FunctionName, bool isFunctionActive)
        //{
        //    _deptRepository = dept;
        //    return _deptRepository.CreateFunction( tenantID,   departmentID,  FunctionName,  isFunctionActive);
        //}

        public int DeleteDepartmentMapping(IDepartment dept, int tenantID,int  DepartmentBrandMappingID)
        {
            _deptRepository = dept;
            return _deptRepository.DeleteDepartmentMapping(tenantID, DepartmentBrandMappingID);
        }


        public int UpdateDepartmentMapping(IDepartment dept, int TenantID, int DepartmentBrandID, int BrandID, int StoreID, int DepartmentID, int FunctionID, bool Status, int CreatedBy)
        {
            _deptRepository = dept;
            return _deptRepository.UpdateDepartmentMapping( TenantID,  DepartmentBrandID,  BrandID,  StoreID,  DepartmentID,  FunctionID,  Status,  CreatedBy);
        }

    }
}
