﻿using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class CustomerChatCaller
    {
        #region Variable declaration

        private ICustomerChat _customerChat;
        #endregion

        #region Methods 
        public List<CustomerChatMaster> OngoingChat(ICustomerChat customerChat,int userMasterID,int tenantID)
        {
            _customerChat = customerChat;

            return _customerChat.OngoingChat(userMasterID, tenantID);
        }

        public List<CustomerChatMaster> NewChat(ICustomerChat customerChat, int userMasterID, int tenantID)
        {
            _customerChat = customerChat;

            return _customerChat.NewChat(userMasterID, tenantID);
        }
        public int MarkAsReadMessage(ICustomerChat customerChat, int chatID)
        {
            _customerChat = customerChat;

            return _customerChat.MarkAsReadOnGoingChat(chatID);
        }
        public int UpdateCustomerChatIdStatus(ICustomerChat customerChatStatus, int chatID, int TenantId)
        {
            _customerChat = customerChatStatus;

            return _customerChat.UpdateCustomerChatIdStatus(chatID, TenantId);
        }
        #endregion
    }
}
