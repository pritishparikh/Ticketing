using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class AppointmentCaller
    {

        #region Variable
        public IAppointment _AppointmentRepository;
        #endregion

        #region Custom method

        /// <summary>
        /// Get Appointment List
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="tenantID"></param>
        /// <param name="UserId"></param>
        /// <param name="AppDate"></param>
        /// <returns></returns>
        public async Task<List<AppointmentModel>> GetAppointmentList(IAppointment appointment,int tenantID, int UserId ,string AppDate)
        {
            _AppointmentRepository = appointment;
            return await _AppointmentRepository.GetAppointmentList(tenantID, UserId, AppDate);
        }

        /// <summary>
        /// Get Appointment Count List
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="tenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public List<AppointmentCount> GetAppointmentCountList(IAppointment appointment, int tenantID,string ProgramCode, int UserId)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GetAppointmentCount(tenantID, ProgramCode,UserId);
        }

        /// <summary>
        /// Update Appointment
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="appointmentCustomer"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public int UpdateAppointmentStatus(IAppointment appointment, AppointmentCustomer appointmentCustomer, int TenantID, string ProgramCode, int UserID)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.UpdateAppointmentStatus(appointmentCustomer, TenantID, ProgramCode, UserID);
        }

        /// <summary>
        /// <summary>
        /// Get Appointment Message Tags
        /// </summary>
        /// <returns></returns>
        public List<AppointmentMessageTag> AppointmentMessageTags(IAppointment appointment)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.AppointmentMessageTags();
        }

        /// <summary>
        /// Get Appointment Search List
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="tenantID"></param>
        /// <param name="UserId"></param>
        /// <param name="appointmentSearchRequest"></param>
        /// <returns></returns>
        public async Task<List<AppointmentModel>> GetAppointmentSearchList(IAppointment appointment, int tenantID, int UserId, AppointmentSearchRequest appointmentSearchRequest)
        {
            _AppointmentRepository = appointment;
            return await _AppointmentRepository.GetAppointmentSearchList(tenantID, UserId, appointmentSearchRequest);
        }

        #region TimeSlotMaster CRUD

        /// <summary>
        /// Insert Update Time Slot Setting
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="Slot"></param>
        /// <returns></returns>
        public int InsertUpdateTimeSlotSetting(IAppointment appointment, StoreTimeSlotInsertUpdate Slot)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.InsertUpdateTimeSlotSetting(Slot);
        }


        /// <summary>
        ///Update Time Slot Setting New
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="Slot"></param>
        /// <returns></returns>
        public async Task<int> UpdateTimeSlotSettingNew(IAppointment appointment, StoreTimeSlotInsertUpdate Slot)
        {
            _AppointmentRepository = appointment;
            return await _AppointmentRepository.UpdateTimeSlotSettingNew(Slot);
        }

        /// <summary>
        /// Delete Time Slot Master
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="SlotIDs"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public async Task<int> BulkDeleteTimeSlotMaster(IAppointment appointment, string SlotIDs, int TenantID, string ProgramCode)
        {
            _AppointmentRepository = appointment;
            return await _AppointmentRepository.BulkDeleteTimeSlotMaster(SlotIDs, TenantID,  ProgramCode);
        }

        /// <summary>
        /// Delete Time Slot Master
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="SlotID"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        public int DeleteTimeSlotMaster(IAppointment appointment, int SlotID, int TenantID, string ProgramCode)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.DeleteTimeSlotMaster(SlotID, TenantID, ProgramCode);
        }


        /// <summary>
        /// Get Store Setting Time Slot
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="SlotID"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public List<StoreTimeSlotSettingModel> GetStoreSettingTimeSlot(IAppointment appointment, int TenantID, string ProgramCode, int SlotID, int StoreID)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GetStoreSettingTimeSlot(TenantID, ProgramCode, SlotID, StoreID);
        }

        /// <summary>
        /// Get Store Setting Time Slot
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="SlotID"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public async Task<List<StoreTimeSlotSettingModel>> GetStoreSettingTimeSlotNew(IAppointment appointment, int TenantID, string ProgramCode, int SlotID, string StoreID, string Opdays, int SlotTemplateID)
        {
            _AppointmentRepository = appointment;
            return await _AppointmentRepository.GetStoreSettingTimeSlotNew(TenantID,  ProgramCode, SlotID,  StoreID,  Opdays,  SlotTemplateID); 
        }


        /// <summary>
        /// Slot bulk upload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="RoleFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        public List<string> BulkUploadSlot(IAppointment appointment, int TenantID, string ProgramCode, int CreatedBy, DataSet DataSetCSV)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.BulkUploadSlot(TenantID, ProgramCode, CreatedBy, DataSetCSV);
        }


        /// <summary>
        /// Slot bulk upload New
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="RoleFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        public async Task<List<string>> BulkUploadSlotNew(IAppointment appointment, int TenantID, string ProgramCode, int CreatedBy, DataSet DataSetCSV)
        {
            _AppointmentRepository = appointment;
            return await _AppointmentRepository.BulkUploadSlotNew(TenantID, ProgramCode, CreatedBy, DataSetCSV);
        }

        /// <summary>
        ///Update Time Slot Setting New
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="Slot"></param>
        /// <returns></returns>
        public async Task<int> BulkUpdateSlots(IAppointment appointment, SlotsBulkUpdate Slots)
        {
            _AppointmentRepository = appointment;
            return await _AppointmentRepository.BulkUpdateSlots(Slots);
        }

        #endregion

        #endregion

    }
}
