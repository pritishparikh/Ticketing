using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IStoreAlerts
    {
        /// <summary>
        /// InsertStoreAlert
        /// </summary>
        /// <param name="alertModel"></param>
        /// <returns></returns>
        int InsertStoreAlert(AlertInsertModel alertModel);

        /// <summary>
        /// UpdateStoreAlert
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="ModifiedBy"></param>
        /// <param name="alertModel"></param>
        /// <returns></returns>
        int UpdateStoreAlert(int tenantId, int ModifiedBy, AlertUpdateModel alertModel);

        /// <summary>
        /// DeleteStoreAlert
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="AlertID"></param>
        /// <returns></returns>
        int DeleteStoreAlert(int tenantID, int AlertID);

        /// <summary>
        /// GetStoreAlertList
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="alertID"></param>
        /// <returns></returns>
        List<AlertModel> GetStoreAlertList(int tenantId, int alertID);

        /// <summary>
        /// BindStoreAlerts
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<AlertList> BindStoreAlerts(int tenantID);

        /// <summary>
        /// BulkStoreUploadAlert
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        int BulkStoreUploadAlert(int TenantID, int CreatedBy, DataSet DataSetCSV);

        /// <summary>
        /// ValidateStoreAlert
        /// </summary>
        /// <param name="AlertID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        string ValidateStoreAlert(int AlertID, int TenantID);
    }
}
