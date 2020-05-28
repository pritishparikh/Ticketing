using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IAppointment
    {

        StoreDetails GetStoreDetailsByStoreCode(int tenantID, int userID, string programcode, string storeCode);
    }
}
