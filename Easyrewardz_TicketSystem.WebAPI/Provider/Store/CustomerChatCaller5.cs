using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class CustomerChatCaller
    {
        #region Methods 

        /// <summary>
        /// SaveReInitiateChat 
        /// </summary>
        /// <param name="customerChatMaster"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <param name="sendText"></param>
        /// <param name="MakeBellActive"></param>
        /// <returns></returns>
        public async Task <int> SaveReInitiateChat(ICustomerChat customerChat, ReinitiateChatModel customerChatMaster,string ClientAPIUrl, string sendText, string MakeBellActive)
        {
            _customerChat = customerChat;
            return await _customerChat.SaveReInitiateChatMessages(customerChatMaster, ClientAPIUrl,  sendText,  MakeBellActive);

        }


        /// <summary>
        /// Get Chat Notifications Details
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <param name="PageNo"></param>
        /// <returns></returns>
        public async Task<MobileNotificationModel> GetMobileNotificationsDetails(ICustomerChat customerChat, int TenantID, string ProgramCode, int UserID, int PageNo)
        {
            _customerChat = customerChat;
            return await _customerChat.GetMobileNotificationsDetails( TenantID,  ProgramCode,  UserID,  PageNo);

        }

        /// <summary>
        /// Update Mobile Notification
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <param name="IndexID"></param>
        /// <returns></returns>
        /// 
        public async Task<int> UpdateMobileNotification(ICustomerChat customerChat, int TenantID, string ProgramCode, int UserID, string IndexID)
        {
            _customerChat = customerChat;
            return await _customerChat.UpdateMobileNotification( TenantID,  ProgramCode,  UserID,  IndexID);

        }
       

        #endregion
    }
}
