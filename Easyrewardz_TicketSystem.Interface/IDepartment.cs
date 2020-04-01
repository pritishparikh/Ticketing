using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IDepartment
    {


        int DeleteDepartmentMapping(int tenantID, int DepartmentBrandMappingID);

        int UpdateDepartmentMapping(int TenantID, int DepartmentBrandID, int BrandID, int StoreID, int DepartmentID, int FunctionID, bool Status, int CreatedBy);


    }
}
