using Easyrewardz_TicketSystem.CustomModel;
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

        List<AppointmentModel> SearchAppointment(int TenantID, int UserId, string searchText, string appointmentDate);

        int GenerateOTP(int TenantID, int UserId, string mobileNumber);

        int VarifyOTP(int TenantID, int UserId, int otpID, string otp);

        int UpdateAppointment(CustomUpdateAppointment appointmentCustomer);

        int ValidateMobileNo(int TenantID, int UserId, string mobileNumber);
    }
}
