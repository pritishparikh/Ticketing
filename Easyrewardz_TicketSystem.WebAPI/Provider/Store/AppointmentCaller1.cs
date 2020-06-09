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

        #region Customer wrapper method 
        public List<AppointmentModel> GetAppointmentList(IAppointment appointment,int tenantID, int UserId ,string AppDate)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GetAppointmentList(tenantID, UserId, AppDate);
        }
             
        /// <summary>
        ///     
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<AppointmentCount> GetAppointmentCountList(IAppointment appointment, int tenantID, int UserId)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GetAppointmentCount(tenantID, UserId);
        }


        public int updateAppoinment(IAppointment appointment, AppointmentCustomer appointmentCustomer, int TenantId)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.UpdateAppointmentStatus(appointmentCustomer, TenantId);
        }



        #region TimeSlotMaster CRUD

        public int InsertUpdateTimeSlotMaster(IAppointment appointment, StoreTimeSlotInsertUpdate Slot)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.InsertUpdateTimeSlotMaster(Slot);
        }

        public int DeleteTimeSlotMaster(IAppointment appointment, int SlotID, int TenantID)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.DeleteTimeSlotMaster(SlotID, TenantID);
        }

        public List<StoreTimeSlotMasterModel> GetStoreTimeSlotMasterList(IAppointment appointment, int TenantID, string ProgramCode, string StoreCode)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.StoreTimeSlotMasterList( TenantID,  ProgramCode, StoreCode); 
        }

        
        #endregion

        #endregion

    }
}
