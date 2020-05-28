using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class AppointmentCaller
    {
        public StoreDetails GetStoreDetailsByStoreCode(IAppointment appointment, int tenantID, int userID, string programcode, string storeCode)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GetStoreDetailsByStoreCode(tenantID, userID, programcode, storeCode);
        }
    }
}
