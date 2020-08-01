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
        /// <summary>
        /// Get Appointment List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserId"></param>
        /// <param name="AppDate"></param>
        /// <returns></returns>
        List<AppointmentModel> GetAppointmentList(int TenantID, int UserId, string AppDate);

        /// <summary>
        /// Get Appointment Count
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        List<AppointmentCount> GetAppointmentCount(int TenantID, string ProgramCode,int UserId);

        /// <summary>
        /// Update Appointment Status
        /// </summary>
        /// <param name="appointmentCustomer"></param>
        /// <param name="TenantId"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int UpdateAppointmentStatus(AppointmentCustomer appointmentCustomer, int TenantID, string ProgramCode, int UserID);


        #region TimeSlotMaster CRUD

        /// <summary>
        /// Insert Update Time Slot Setting
        /// </summary>
        /// <param name="Slot"></param>
        /// <returns></returns>
        int InsertUpdateTimeSlotSetting(StoreTimeSlotInsertUpdate Slot);

        /// <summary>
        /// Delete Time Slot Master
        /// </summary>
        /// <param name="SlotID"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        int DeleteTimeSlotMaster(int SlotID, int TenantID, string ProgramCode);

        /// <summary>
        /// Get Store Setting Time Slot
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="SlotID"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        List<StoreTimeSlotSettingModel> GetStoreSettingTimeSlot(int TenantID, string ProgramCode, int SlotID, int StoreID);

        #endregion
    }
}
