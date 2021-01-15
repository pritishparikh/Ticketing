using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStoreCRMRole
    {
        /// <summary>
        /// Insert Update Store CRM Role
        /// </summary>
        /// <param name="CRMRoleID"></param>
        /// <param name="tenantID"></param>
        /// <param name="RoleName"></param>
        /// <param name="RoleisActive"></param>
        /// <param name="createdBy"></param>
        /// <param name="ModulesEnabled"></param>
        /// <param name="ModulesDisabled"></param>
        /// <returns></returns>
        int InsertUpdateStoreCRMRole(int CRMRoleID, int tenantID, string RoleName, bool RoleisActive, int createdBy, string ModulesEnabled, string ModulesDisabled);

        /// <summary>
        /// Delete Store CRM Role
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="CRMRoleID"></param>
        /// <returns></returns>
        int DeleteStoreCRMRole(int tenantID, int CRMRoleID);

        /// <summary>
        /// Get Store CRM Role List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<StoreCRMRoleModel> GetStoreCRMRoleList(int tenantID);

        /// <summary>
        /// Get Store CRM Role Dropdown
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<StoreCRMRoleModel> GetStoreCRMRoleDropdown(int tenantID);

        /// <summary>
        /// Get Store CRM Role By User ID
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        StoreCRMRoleModel GetStoreCRMRoleByUserID(int tenantID, int UserID);

        /// <summary>
        /// Store Bulk Upload CRM Role
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="RoleFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> StoreBulkUploadCRMRole(int TenantID, int CreatedBy, int RoleFor, DataSet DataSetCSV);

        /// <summary>
        /// Get Store Crm Module
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<CrmModule> GetStoreCrmModule(int tenantID);
    }
}
