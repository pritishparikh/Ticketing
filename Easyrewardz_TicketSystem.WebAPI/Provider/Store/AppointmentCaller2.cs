using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class AppointmentCaller
    {
        public StoreDetails GetStoreDetailsByStoreCode(IAppointment appointment, int tenantID, int userID, string programcode, string storeCode)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GetStoreDetailsByStoreCode(tenantID, userID, programcode, storeCode);
        }

        public List<SlotTemplateModel> GetSlotTemplates(IAppointment appointment, int TenantID, string ProgramCode)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GetSlotTemplates( TenantID,  ProgramCode);
        }
    }
}
