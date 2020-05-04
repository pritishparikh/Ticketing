using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IAppointment
    {
        List<AppointmentModel> GetAppointmentList(int TenantID, string AppDate);

        List<AppointmentCustomer> GetAppointmentCustomerList(int TenantID, string AppDate, int SlotId);

        List<AppointmentCount> GetAppointmentCount(int TenantID);

        int UpdateAppointmentStatus(AppointmentCustomer appointmentCustomer, int TenantId);
    }
}
