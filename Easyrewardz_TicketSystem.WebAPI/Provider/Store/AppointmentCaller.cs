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
        public List<AppointmentModel> GetAppointmentList(IAppointment appointment,int tenantID,string AppDate)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GetAppointmentList(tenantID, AppDate);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<AppointmentCount> GetAppointmentCountList(IAppointment appointment, int tenantID)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.GetAppointmentCount(tenantID);
        }


        public int updateAppoinment(IAppointment appointment, AppointmentCustomer appointmentCustomer, int TenantId)
        {
            _AppointmentRepository = appointment;
            return _AppointmentRepository.UpdateAppointmentStatus(appointmentCustomer, TenantId);
        }



        #endregion

    }  
}
