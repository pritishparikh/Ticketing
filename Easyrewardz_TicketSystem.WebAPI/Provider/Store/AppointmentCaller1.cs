using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
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
        public List<AppointmentModel> GetAppointmentList(IAppointment appointment,int tenantID, int UserId ,string AppDate)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GetAppointmentList(tenantID, UserId, AppDate);
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
        public int UpdateAppoinment(IAppointment appointment, AppointmentCustomer appointmentCustomer, int TenantId)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.UpdateAppointmentStatus(appointmentCustomer, TenantId);
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
            return _AppointmentRepository.DeleteTimeSlotMaster(SlotID, TenantID,  ProgramCode);
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
            return _AppointmentRepository.GetStoreSettingTimeSlot(TenantID,  ProgramCode, SlotID, StoreID); 
        }

        
        #endregion

        #endregion

    }
}
