using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
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

        #endregion

    }  
}
