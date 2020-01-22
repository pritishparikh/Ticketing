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
        private ISLA _SLA;

        #endregion


        #region CRMRoles
        /// <summary>
        /// InsertCRMRole
        /// </summary>
        public int InsertUpdateCRMRole(ICRMRole CRM, int CRMRoleID, int tenantID, string RoleName, bool RoleisActive, int UserID, string ModulesEnabled, string ModulesDisabled)
        {
            _dCRMrole = CRM;
                
            return _dCRMrole.InsertUpdateCRMRole(CRMRoleID,tenantID, RoleName, RoleisActive, UserID, ModulesEnabled, ModulesDisabled);
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

        #region SLA
        public int InsertSLA(ISLA SLA, SLAModel SLAm)
        {
            _SLA = SLA;

            return _SLA.InsertSLA( SLAm);
        }

        public int UpdateSLA(ISLA SLA, int tenantID, int SLAID, int IssuetypeID, bool isActive, int modifiedBy)
        {
            _SLA = SLA;
            return _SLA.UpdateSLA(SLAID, tenantID, IssuetypeID, isActive, modifiedBy);
        }

        public int DeleteSLA(ISLA SLA, int tenantID, int SLAID)
        {
            _SLA = SLA;
            return _SLA.DeleteSLA(tenantID, SLAID);

        }

        public List<SLAResponseModel> SLAList(ISLA SLA,int TenantID)
        {
            _SLA = SLA;
            return _SLA.SLAList(TenantID);
        }
        #endregion



    }
}
