using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ICRMRole
    {
        /// <summary>
        /// Insert Update CRM Role
        /// </summary>
        /// <param name="CRMRoleID"></param>
        /// <param name="tenantID"></param>
        /// <param name="RoleName"></param>
        /// <param name="RoleisActive"></param>
        /// <param name="createdBy"></param>
        /// <param name="ModulesEnabled"></param>
        /// <param name="ModulesDisabled"></param>
        /// <returns></returns>
        int InsertUpdateCRMRole(int CRMRoleID, int tenantID, string RoleName, bool RoleisActive, int createdBy, string ModulesEnabled, string ModulesDisabled);

        /// <summary>
        /// Delete CRM Role
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="CRMRoleID"></param>
        /// <returns></returns>
        int DeleteCRMRole(int tenantID, int CRMRoleID);

        /// <summary>
        /// Get CRM Role List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<CRMRoleModel> GetCRMRoleList(int tenantID);

        /// <summary>
        /// Get CRM Role Dropdown
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<CRMRoleModel> GetCRMRoleDropdown(int tenantID);

        /// <summary>
        /// Get CRM Role By User ID
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        CRMRoleModel GetCRMRoleByUserID(int tenantID, int UserID);

        /// <summary>
        /// Bulk Upload CRM Role
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="RoleFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> BulkUploadCRMRole(int TenantID, int CreatedBy, int RoleFor, DataSet DataSetCSV);
    }
}
