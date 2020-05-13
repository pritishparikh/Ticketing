using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Provider.Store
{
    public class StoreCRMRoleCaller
    {
        #region Variable declaration

        private IStoreCRMRole _dCRMrole;
       
        #endregion

        #region Store CRMRoles
        /// <summary>
        /// InsertCRMRole
        /// </summary>
        public int InsertUpdateCRMRole(IStoreCRMRole CRM, int CRMRoleID, int tenantID, string RoleName, bool RoleisActive, int UserID, string ModulesEnabled, string ModulesDisabled)
        {
            _dCRMrole = CRM;

            return _dCRMrole.InsertUpdateStoreCRMRole(CRMRoleID, tenantID, RoleName, RoleisActive, UserID, ModulesEnabled, ModulesDisabled);
        }
        /// <summary>
        /// DeleteCRMRole
        /// </summary>
        public int DeleteCRMRole(IStoreCRMRole CRM, int tenantID, int CRMRoleID)
        {
            _dCRMrole = CRM;
            return _dCRMrole.DeleteStoreCRMRole(tenantID, CRMRoleID);

        }
        /// <summary>
        /// CRMRoleList
        /// </summary>
        public List<StoreCRMRoleModel> CRMRoleList(IStoreCRMRole CRM, int tenantID)
        {
            _dCRMrole = CRM;
            return _dCRMrole.GetStoreCRMRoleList(tenantID);

        }

        public StoreCRMRoleModel GetCRMRoleByUserID(IStoreCRMRole CRM, int tenantID, int UserID)
        {
            _dCRMrole = CRM;
            return _dCRMrole.GetStoreCRMRoleByUserID(tenantID, UserID);

        }
        public List<StoreCRMRoleModel> CRMRoleDropdown(IStoreCRMRole CRM, int tenantID)
        {
            _dCRMrole = CRM;
            return _dCRMrole.GetStoreCRMRoleDropdown(tenantID);

        }

        public List<string> CRMRoleBulkUpload(IStoreCRMRole CRM, int TenantID, int CreatedBy, int RoleFor, DataSet DataSetCSV)
        {
            _dCRMrole = CRM;
            return _dCRMrole.StoreBulkUploadCRMRole(TenantID, CreatedBy, RoleFor, DataSetCSV);
        }

        public List<CrmModule> GetStoreCrmModule(IStoreCRMRole CRM, int tenantID)
        {
            _dCRMrole = CRM;
            return _dCRMrole.GetStoreCrmModule(tenantID);
        }

        #endregion
    }
}
