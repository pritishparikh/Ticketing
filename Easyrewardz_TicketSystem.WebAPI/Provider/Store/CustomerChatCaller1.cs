﻿using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public List<CustomerChatMaster> OngoingChat(ICustomerChat customerChat,int userMasterID,int tenantID)
        {
            _customerChat = customerChat;

            return _customerChat.OngoingChat(userMasterID, tenantID);
        }

        /// <summary>
        /// Get New Chat
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CustomerChatMaster> NewChat(ICustomerChat customerChat, int userMasterID, int tenantID)
        {
            _customerChat = customerChat;

            return _customerChat.NewChat(userMasterID, tenantID);
        }

        /// <summary>
        /// Read On Going Message
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int MarkAsReadMessage(ICustomerChat customerChat, int chatID)
        {
            _customerChat = customerChat;

            return _customerChat.MarkAsReadOnGoingChat(chatID);
        }

        /// <summary>
        /// UpdateCustomerChatStatus
        /// </summary>
        /// <param name="chatid"></param>
        /// <returns></returns>
        public int UpdateCustomerChatIdStatus(ICustomerChat customerChatStatus, int chatID, int TenantId)
        {
            _customerChat = customerChatStatus;

            return _customerChat.UpdateCustomerChatIdStatus(chatID, TenantId);
        }

        /// <summary>
        /// Schedule Visit 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public string ScheduleVisit(ICustomerChat customerChatStatus, AppointmentMaster appointmentMaster)
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
        public int GetChatCount(ICustomerChat customerChat,int tenantID)
        {

            _customerChat = customerChat;

            return _customerChat.GetChatCount(tenantID);
        }

        /// <summary>
        /// Get Time Slot
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public List<TimeSlotModel> GetTimeSlot(ICustomerChat customerChat, int storeID, int userMasterID, int tenantID)
        {
            _customerChat = customerChat;

            return _customerChat.GetTimeSlot(storeID,userMasterID, tenantID);
        }

        #endregion
    }
}
