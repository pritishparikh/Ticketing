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
        
        List<AppointmentModel> GetAppointmentList(int TenantID, int UserId, string AppDate);

        List<AppointmentCount> GetAppointmentCount(int TenantID, int UserId);

        int UpdateAppointmentStatus(AppointmentCustomer appointmentCustomer, int TenantId);

        List<AppointmentDetails> CreateAppointment(AppointmentMaster appointmentMaster, bool IsSMS, bool IsLoyalty);

        List<AppointmentDetails> CreateNonExistCustAppointment(AppointmentMaster appointmentMaster, bool IsSMS, bool IsLoyalty);

        List<AppointmentModel> SearchAppointment(int TenantID, int UserId, string searchText, string appointmentDate);

        int GenerateOTP(int TenantID, int UserId, string mobileNumber);

        int VarifyOTP(int TenantID, int UserId, int otpID, string otp);

        List<AlreadyScheduleDetail> GetTimeSlotDetail(int userMasterID, int tenantID, string AppDate);

        int UpdateAppointment(CustomUpdateAppointment appointmentCustomer);

        int ValidateMobileNo(int TenantID, int UserId, string mobileNumber);

        int StartVisit(CustomUpdateAppointment appointmentCustomer);

        List<CustomerCountDetail> GetCustomerInStore(int TenantID, int UserId);

        #region TimeSlotMaster CRUD

        int InsertUpdateTimeSlotMaster(StoreTimeSlotInsertUpdate Slot);


        int DeleteTimeSlotMaster(int SlotID, int TenantID);

        List<StoreTimeSlotMasterModel> StoreTimeSlotMasterList(int TenantID, string ProgramCode);

        #endregion
    }
}
