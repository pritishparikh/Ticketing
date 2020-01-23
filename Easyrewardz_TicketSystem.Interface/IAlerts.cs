using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
   public interface IAlerts
    {
        //int InsertAlert(int tenantId, string AlerttypeName, string CommunicationMode, string CommunicationFor, 
        //    string Content, string ToEmailID, string CCEmailID, string BCCEmailID, string Subject, string IsActive, int CreatedBy);


        int UpdateAlert(int tenantId, int AlertID, string AlertTypeName, bool isAlertActive, int ModifiedBy);

        int DeleteAlert(int tenantID, int AlertID);

        //List<AlertModel> GetAlertList(int tenantId);
    }
}
