using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class SettingsCaller
    {
        #region Variable declaration

        private ICRMRole _dCRMrole;

        #endregion


        #region CRMRoles
        /// <summary>
        /// InsertCRMRole
        /// </summary>
        public int InsertCRMRole(ICRMRole CRM, int tenantID, string RoleName, bool RoleisActive, int createdBy, string ModulesEnabled, string ModulesDisabled)
        {
            _dCRMrole = CRM;
                
            return _dCRMrole.InsertCRMRole(tenantID, RoleName, RoleisActive, createdBy, ModulesEnabled, ModulesDisabled);
        }
        /// <summary>
        /// UpdateCRMRole
        /// </summary>
        public int UpdateCRMRole(ICRMRole CRM, int tenantID, int CRMRoleID, string RoleName, string Modules, bool RoleisActive, int modifiedBy)
        {
            _dCRMrole = CRM;
            return _dCRMrole.UpdateCRMRole(tenantID,CRMRoleID,RoleName,Modules,RoleisActive,modifiedBy);
        }
        /// <summary>
        /// DeleteCRMRole
        /// </summary>
        public int DeleteCRMRole(ICRMRole CRM, int tenantID, int CRMRoleID)
        {
            _dCRMrole = CRM;
            return _dCRMrole.DeleteCRMRole(tenantID, CRMRoleID);

        }
        /// <summary>
        /// CRMRoleList
        /// </summary>
        public List<CRMRoleModel> CRMRoleList(ICRMRole CRM, int tenantID)
        {
            _dCRMrole = CRM;
            return _dCRMrole.GetCRMRoleList(tenantID);

        }

        #endregion



    }
}
