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

        private ITemplate _Temp;

        private IAlerts _Alerts;

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
            return _SLA.SLAList(TenantID );
        }

        public List<IssueTypeList> BindIssueTypeList(ISLA SLA,int tenantID)
        {
            _SLA = SLA;
            return _SLA.BindIssueTypeList(tenantID);
        }

        //public List<SLAResponseModel> SLAList(ISLA SLA, int TenantID, int pageNo, int PageSize)
        //{
        //    _SLA = SLA;
        //    return _SLA.SLAList(TenantID, pageNo, PageSize);
        //}
        #endregion


        #region Templates

        public int InsertTemplate(ITemplate Temp, int tenantId, string TemplateName, string TemplatSubject, string TemplatBody, string issueTypes, bool isTemplateActive, int createdBy)
        {
            _Temp = Temp;

            return _Temp.InsertTemplate( tenantId,  TemplateName,  TemplatSubject,  TemplatBody,  issueTypes,  isTemplateActive,  createdBy);
        }

        public int UpdateTemplate(ITemplate Temp, int tenantId, int TemplateID, string TemplateName, int issueType, bool isTemplateActive, int ModifiedBy)
        {
            _Temp = Temp;
            return _Temp.UpdateTemplate( tenantId,  TemplateID,  TemplateName,  issueType,  isTemplateActive,  ModifiedBy);
        }

        public int DeleteTemplate(ITemplate Temp, int tenantID, int TemplateID)
        {
            _Temp = Temp;
            return _Temp.DeleteTemplate(tenantID, TemplateID);

        }

        public List<TemplateModel> GetTemplates(ITemplate Temp, int TenantID)
        {
            _Temp = Temp;
            return _Temp.GetTemplates(TenantID);
        }
        #endregion


        #region Alerts

        /// <summary>
        /// UpdateAlert
        /// </summary>
        //public int CreateAlert(IAlerts Alert, int tenantId, int AlertID, string AlertTypeName, bool isAlertActive, int ModifiedBy)
        //{
        //    _Alerts = Alert;
        //    return _Alerts.UpdateAlert(tenantId, AlertID, AlertTypeName, isAlertActive, ModifiedBy);

        //}


        /// <summary>
        /// UpdateAlert
        /// </summary>
        public int UpdateAlert(IAlerts Alert, int tenantId, int AlertID, string AlertTypeName, bool isAlertActive, int ModifiedBy)
        {
            _Alerts = Alert;
            return _Alerts.UpdateAlert( tenantId,  AlertID,  AlertTypeName,  isAlertActive,  ModifiedBy);

        }

        /// <summary>
        /// DeleteAlert
        /// </summary>
        public int DeleteAlert(IAlerts Alert, int tenantID, int AlertID)
        {
            _Alerts = Alert;
            return _Alerts.DeleteAlert( tenantID,  AlertID);

        }

        /// <summary>
        /// Get AlertList
        /// </summary>
        //public List<AlertModel> GetAlertList(IAlerts Alert, int tenantID)
        //{
        //    _Alerts = Alert;
        //    return _Alerts.GetAlertList(tenantID);

        //}

        #endregion

    }
}
