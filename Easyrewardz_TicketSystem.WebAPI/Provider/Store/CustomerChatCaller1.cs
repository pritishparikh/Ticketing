using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class CustomerChatCaller
    {
        #region Variable declaration

        private ICustomerChat _customerChat;
        #endregion

        #region Methods 
        /// <summary>
        /// Get Ongoing Chat
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CustomerChatMaster> OngoingChat(ICustomerChat customerChat,int UserMasterID,int TenantID, string Search,int StoreManagerID)
        {
            _customerChat = customerChat;

            return _customerChat.OngoingChat(UserMasterID, TenantID, Search, StoreManagerID);
        }

        /// <summary>
        /// Get New Chat
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CustomerChatMaster> NewChat(ICustomerChat customerChat, int UserMasterID, int TenantID)
        {
            _customerChat = customerChat;

            return _customerChat.NewChat(UserMasterID, TenantID);
        }

        /// <summary>
        /// Read On Going Message
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int MarkAsReadMessage(ICustomerChat customerChat, int ChatID)
        {
            _customerChat = customerChat;

            return _customerChat.MarkAsReadOnGoingChat(ChatID);
        }

        /// <summary>
        /// UpdateCustomerChatStatus
        /// </summary>
        /// <param name="chatid"></param>
        /// <returns></returns>
        public int UpdateCustomerChatIdStatus(ICustomerChat customerChatStatus, int ChatID, int TenantId, int UserID)
        {
            _customerChat = customerChatStatus;

            return _customerChat.UpdateCustomerChatIdStatus(ChatID, TenantId, UserID);
        }

        /// <summary>
        /// Schedule Visit 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<AppointmentDetails> ScheduleVisit(ICustomerChat customerChatStatus, AppointmentMaster appointmentMaster)
        {
            _customerChat = customerChatStatus;

            return _customerChat.ScheduleVisit(appointmentMaster);
        }

        /// <summary>
        /// CustomerChatHistory
        /// </summary>
        /// <param name="ChatId"></param>
        /// <returns></returns>
        public List<CustomerChatHistory> CustomerChatHistory(ICustomerChat customerchatHistory, int ChatID)
        {
            _customerChat = customerchatHistory;
            return _customerChat.CustomerChatHistory(ChatID);
        }

        /// <summary>
        /// Get Chat Notification Count
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        public int GetChatCount(ICustomerChat customerChat,int TenantID,int UserMasterID)
        {

            _customerChat = customerChat;

            return _customerChat.GetChatCount(TenantID, UserMasterID);
        }

        /// <summary>
        /// Get Time Slot
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<DateofSchedule> GetTimeSlot(ICustomerChat customerChat, int TenantID, string Programcode, int UserID)
        {
            _customerChat = customerChat;

            return _customerChat.GetTimeSlot( TenantID,  Programcode, UserID);
        }

        /// <summary>
        /// Send Message To Customer For Visit
        /// </summary>
        /// <param name="customerChat"></param>
        /// <param name="appointmentMaster"></param>
        /// <param name="ClientAPIURL"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        public int SendMessageToCustomerForVisit(ICustomerChat customerChat, AppointmentMaster appointmentMaster, string ClientAPIURL, int CreatedBy)
        {
            _customerChat = customerChat;
            return _customerChat.SendMessageToCustomerForVisit(appointmentMaster, ClientAPIURL, CreatedBy);

        }
        #endregion
    }
}
