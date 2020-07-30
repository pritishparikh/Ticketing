using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {
        /// <summary>
        /// Ongoing Chat
        /// </summary>
        /// <param name="userMasterID"></param>
        /// <param name="tenantID"></param>
        /// <param name="Search"></param>
        /// <param name="StoreManagerID"></param>
        /// <returns></returns>
        List<CustomerChatMaster> OngoingChat(int userMasterID,int tenantID, string Search, int StoreManagerID);

        /// <summary>
        /// New Chat
        /// </summary>
        /// <param name="userMasterID"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<CustomerChatMaster> NewChat(int userMasterID, int tenantID);

        /// <summary>
        /// Mark As Read OnGoing Chat
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        int MarkAsReadOnGoingChat(int chatID);

        /// <summary>
        /// Update Customer Chat Id Status
        /// </summary>
        /// <param name="chatID"></param>
        /// <param name="tenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int UpdateCustomerChatIdStatus(int chatID, int tenantID, int UserID);

        /// <summary>
        /// Schedule Visit
        /// </summary>
        /// <param name="appointmentMaster"></param>
        /// <returns></returns>
        List<AppointmentDetails> ScheduleVisit(AppointmentMaster appointmentMaster);

        /// <summary>
        /// Customer Chat History
        /// </summary>
        /// <param name="chatID"></param>
        /// <returns></returns>
        List<CustomerChatHistory> CustomerChatHistory(int chatID);

        /// <summary>
        /// Get Chat Count
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="UserMasterID"></param>
        /// <returns></returns>
        int GetChatCount(int tenantID, int UserMasterID);

        /// <summary>
        /// Get Time Slot
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="Programcode"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<DateofSchedule> GetTimeSlot(int TenantID, string Programcode, int UserID);

        /// <summary>
        /// Send Message To Customer For Visit
        /// </summary>
        /// <param name="appointmentMaster"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        int SendMessageToCustomerForVisit(AppointmentMaster appointmentMaster, string ClientAPIURL, int CreatedBy);
    }
}
