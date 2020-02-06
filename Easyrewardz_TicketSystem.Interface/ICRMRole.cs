using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ICRMRole
    {
        int InsertUpdateCRMRole(int CRMRoleID, int tenantID, string RoleName, bool RoleisActive, int createdBy, string ModulesEnabled, string ModulesDisabled);

      
        int DeleteCRMRole(int tenantID, int CRMRoleID);

        List<CRMRoleModel> GetCRMRoleList(int tenantID);
        List<CRMRoleModel> GetCRMRoleDropdown(int tenantID);

        int BulkUploadCRMRole(int TenantID, int CreatedBy, DataSet DataSetCSV);
    }
}
