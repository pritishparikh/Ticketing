using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ICRMRole
    {
        int InsertCRMRole(int tenantID, string RoleName, bool RoleisActive, int createdBy, string ModulesEnabled, string ModulesDisabled);

        int UpdateCRMRole( int tenantID, int CRMRoleID, string RoleName, string Modules, bool RoleisActive, int modifiedBy);

        int DeleteCRMRole(int tenantID, int CRMRoleID);

        List<CRMRoleModel> GetCRMRoleList(int tenantID);

    }
}
