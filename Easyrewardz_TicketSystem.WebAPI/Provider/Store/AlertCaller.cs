using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class AlertCaller
    {

        private IStoreAlerts _Alerts;

        #region Alerts

        /// <summary>
        /// Create Store Alert
        /// </summary>
        public int CreateStoreAlert(IStoreAlerts Alert, AlertInsertModel alertModel)
        {
            _Alerts = Alert;
            return _Alerts.InsertStoreAlert(alertModel);

        }      

        /// <summary>
        /// Update store Alert
        /// </summary>
        public int UpdateStoreAlert(IStoreAlerts Alert, int tenantId, int ModifiedBy, AlertUpdateModel alertModel)
        {
            _Alerts = Alert;
            //return _Alerts.UpdateAlert( tenantId, AlertID, AlertTypeName, isAlertActive, ModifiedBy);
            return _Alerts.UpdateStoreAlert(tenantId, ModifiedBy, alertModel);


        }

        /// <summary>
        /// Delete store Alert
        /// </summary>
        public int DeleteStoreAlert(IStoreAlerts Alert, int tenantID, int AlertID)
        {
            _Alerts = Alert;
            return _Alerts.DeleteStoreAlert(tenantID, AlertID);

        }

        /// <summary>
        /// Get Store AlertList For Grid
       /// </summary>
        public List<AlertModel> GetStoreAlertList(IStoreAlerts Alert, int tenantID, int alertID)
        {
            _Alerts = Alert;
            return _Alerts.GetStoreAlertList(tenantID, alertID);

        }

        /// <summary>
        /// Get Store AlertList For DropDown
        /// </summary>
        public List<AlertList> BindStoreAlerts(IStoreAlerts Alert, int tenantID)
        {
            _Alerts = Alert;
            return _Alerts.BindStoreAlerts(tenantID);

        }

        /// <summary>
        /// Bulk Upload Store Alert in DB
       /// </summary>
        public int BulkUploadStoreAlert(IStoreAlerts alerts, int TenantID, int CreatedBy, DataSet DataSetCSV)
        {
            _Alerts = alerts;
            return _Alerts.BulkStoreUploadAlert(TenantID, CreatedBy, DataSetCSV);
        }

        /// <summary>
        /// Validate Store Alert Name Exist
        /// </summary>
        public string ValidateStoreAlertNameExist(IStoreAlerts Alert, int AlertID, int tenantID)
        {
            _Alerts = Alert;
            return _Alerts.ValidateStoreAlert(AlertID, tenantID);

        }
        #endregion
    }
}
