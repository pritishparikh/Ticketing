using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {
        /// <summary>
        /// SaveReInitiateChatMessages
        /// </summary>
        /// <param name="customerChatMaster"></param>
        /// <param name="ClientAPIUrl"></param>
        /// <param name="sendText"></param>
        /// <param name="MakeBellActive"></param>
        /// <returns></returns>
        Task<int> SaveReInitiateChatMessages(ReinitiateChatModel customerChatMaster, string ClientAPIUrl,string sendText,string MakeBellActive);


        /// <summary>
        /// Get Mobile Notifications Details
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <param name="PageNo"></param>
        /// <returns></returns>
        Task<MobileNotificationModel> GetMobileNotificationsDetails(int TenantID, string ProgramCode, int UserID, int PageNo);

        /// <summary>
        /// Update Mobile sNotification
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <param name="UserID"></param>
        /// <param name="IndexID"></param>
        /// <returns></returns>
        Task<int> UpdateMobileNotification( int TenantID, string ProgramCode, int UserID,string IndexID);
    }
}
