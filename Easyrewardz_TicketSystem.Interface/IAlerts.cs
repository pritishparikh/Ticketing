using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IAlerts
    {
        /// <summary>
        /// Insert Alert
        /// </summary>
        /// <param name="alertModel"></param>
        /// <returns></returns>
        int InsertAlert(AlertInsertModel alertModel);

        /// <summary>
        /// Validate Alert
        /// </summary>
        /// <param name="AlertID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        string ValidateAlert(int AlertID, int TenantID);

        /// <summary>
        /// Update Alert
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="ModifiedBy"></param>
        /// <param name="alertModel"></param>
        /// <returns></returns>
        int UpdateAlert(int tenantId, int ModifiedBy, AlertUpdateModel alertModel);

        /// <summary>
        /// Delete Alert
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="AlertID"></param>
        /// <returns></returns>
        int DeleteAlert(int tenantID, int AlertID);

        /// <summary>
        /// Get Alert List
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="alertID"></param>
        /// <returns></returns>
        List<AlertModel> GetAlertList(int tenantId, int alertID);

        /// <summary>
        /// Bind Alerts
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<AlertList> BindAlerts(int tenantID);

        /// <summary>
        /// Bulk Upload Alert
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        int BulkUploadAlert(int TenantID, int CreatedBy, DataSet DataSetCSV);
    }
}
