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

        List<AlreadyScheduleDetail> GetTimeSlotDetail(int userMasterID, int tenantID, string AppDate);


        #region TimeSlotMaster CRUD

        int InsertUpdateTimeSlotMaster(StoreTimeSlotInsertUpdate Slot);


        int DeleteTimeSlotMaster(int SlotID, int TenantID);

        List<StoreTimeSlotMasterModel> StoreTimeSlotMasterList(int TenantID, string ProgramCode);

        #endregion
    }
}
