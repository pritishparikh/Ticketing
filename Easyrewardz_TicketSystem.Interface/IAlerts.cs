﻿using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IAlerts
    {
        int InsertAlert(AlertInsertModel alertModel);


        int UpdateAlert(int tenantId, int AlertID, string AlertTypeName, bool isAlertActive, int ModifiedBy);

        int DeleteAlert(int tenantID, int AlertID);

        List<AlertModel> GetAlertList(int tenantId);

        List<AlertList> BindAlerts(int tenantID);

        int BulkUploadAlert(int TenantID, int CreatedBy, DataSet DataSetCSV);
    }
}
