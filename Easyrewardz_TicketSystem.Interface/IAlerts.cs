using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IAlerts
    {
        int InsertAlert(AlertInsertModel alertModel);

        string ValidateAlert(int AlertID, int TenantID);

        int UpdateAlert(int tenantId, int ModifiedBy, AlertUpdateModel alertModel);

        int DeleteAlert(int tenantID, int AlertID);

        List<AlertModel> GetAlertList(int tenantId, int alertID);

        List<AlertList> BindAlerts(int tenantID);

        int BulkUploadAlert(int TenantID, int CreatedBy, DataSet DataSetCSV);
    }
}
