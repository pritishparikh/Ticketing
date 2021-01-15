using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

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


        /// <summary>
        /// Get Appointment Message Tags
        /// </summary>
        /// <returns></returns>
        List<AppointmentMessageTag> AppointmentMessageTags();


        #region TimeSlotMaster CRUD

        /// <summary>
        /// Insert Update Time Slot Setting
        /// </summary>
        /// <param name="Slot"></param>
        /// <returns></returns>
        int InsertUpdateTimeSlotSetting(StoreTimeSlotInsertUpdate Slot);


        /// <summary>
        /// Insert Update Time Slot Setting New
        /// </summary>
        /// <param name="Slot"></param>
        /// <returns></returns>
         Task<int> UpdateTimeSlotSettingNew(StoreTimeSlotInsertUpdate Slot);

        /// <summary>
        /// Delete Time Slot Master
        /// </summary>
        /// <param name="SlotID"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        int DeleteTimeSlotMaster(int SlotID, int TenantID, string ProgramCode);

        /// <summary>
        ///Bulk  Delete Time Slot Master
        /// </summary>
        /// <param name="SlotIDs"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        Task<int> BulkDeleteTimeSlotMaster(string SlotIDs, int TenantID, string ProgramCode); 

        /// <summary>
        /// Get Store Setting Time Slot
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="SlotID"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        List<StoreTimeSlotSettingModel> GetStoreSettingTimeSlot(int TenantID, string ProgramCode, int SlotID, int StoreID);

        /// <summary>
        /// Get Store Setting Time Slot
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="SlotID"></param>
        /// <param name="StoreID"></param>
        ///  <param name="Opdays"></param>
        ///   <param name="SlotTemplateID"></param>
        /// <returns></returns>
        Task<List<StoreTimeSlotSettingModel>> GetStoreSettingTimeSlotNew(int TenantID, string ProgramCode, int SlotID, string StoreID, string Opdays, int SlotTemplateID);


        /// <summary>
        /// slot bulk upload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="RoleFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> BulkUploadSlot(int TenantID,string ProgramCode, int CreatedBy, DataSet DataSetCSV);


        /// <summary>
        /// slot bulk upload New
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="RoleFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        Task<List<string>> BulkUploadSlotNew(int TenantID, string ProgramCode, int CreatedBy, DataSet DataSetCSV);

        /// <summary>
        /// Bulk Update Slots 
        /// </summary>
        /// <param name="Slot"></param>
        /// <returns></returns>
        Task<int> BulkUpdateSlots(SlotsBulkUpdate Slots);

        #endregion
    }
}
