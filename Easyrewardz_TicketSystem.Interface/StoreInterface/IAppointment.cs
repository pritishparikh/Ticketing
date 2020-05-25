using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IAppointment
    {
        List<AppointmentModel> GetAppointmentList(int TenantID, int UserId, string AppDate);

        List<AppointmentCount> GetAppointmentCount(int TenantID, int UserId);

        int UpdateAppointmentStatus(AppointmentCustomer appointmentCustomer, int TenantId);

        List<AppointmentDetails> CreateAppointment(AppointmentMaster appointmentMaster);
    }
}
