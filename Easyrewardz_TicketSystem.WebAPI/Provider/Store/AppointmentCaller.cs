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
    public class AppointmentCaller
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

        public List<AppointmentModel> SearchAppointment(IAppointment appointment, int tenantID, int UserId, string searchText, string appointmentDate)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.SearchAppointment(tenantID, UserId, searchText, appointmentDate);
        }
        public int GenerateOTP(IAppointment appointment, int tenantID, int UserId, string mobileNumber )
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GenerateOTP(tenantID, UserId, mobileNumber);
        }

        public int VarifyOTP(IAppointment appointment, int tenantID, int UserId, int otpID, string otp)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.VarifyOTP(tenantID, UserId, otpID, otp);
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

        public List<AppointmentDetails> CreateAppointment(IAppointment appointment, AppointmentMaster appointmentMaster)
        {
            _AppointmentRepository = appointment;

            return _AppointmentRepository.CreateAppointment(appointmentMaster);
        }

        public List<AppointmentDetails> CreateNonExistCustAppointment(IAppointment appointment, AppointmentMaster appointmentMaster, bool IsSMS, bool IsLoyalty)
        {
            _AppointmentRepository = appointment;

            return _AppointmentRepository.CreateNonExistCustAppointment(appointmentMaster, IsSMS, IsLoyalty);
        }

        public int AppoinmentStatus(IAppointment appointment, CustomUpdateAppointment appointmentCustomer)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.UpdateAppointment(appointmentCustomer);
        }

        public int StartVisit(IAppointment appointment, CustomUpdateAppointment appointmentCustomer)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.StartVisit(appointmentCustomer);
        }

        public List<AlreadyScheduleDetail> GetTimeSlotDetail(IAppointment appointment, int userMasterID, int tenantID, string AppDate)
        {
            _AppointmentRepository = appointment;

            return _AppointmentRepository.GetTimeSlotDetail(userMasterID, tenantID, AppDate);
        }
        public int ValidateMobileNo(IAppointment appointment, int tenantID, int UserId, string mobileNumber)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.ValidateMobileNo(tenantID, UserId, mobileNumber);
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

        public List<StoreTimeSlotMasterModel> GetStoreTimeSlotMasterList(IAppointment appointment, int TenantID, string ProgramCode)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.StoreTimeSlotMasterList( TenantID,  ProgramCode);
        }

        
        #endregion

        #endregion

    }
}
