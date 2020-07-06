using Easyrewardz_TicketSystem.Interface;
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

        public ChatCustomerProfileModel GetChatCustomerProfileDetails(ICustomerChat customerChat, int TenantID, string Programcode, int CustomerID, int UserID, string ClientAPIURL)
        {
            _customerChat = customerChat;
            return _customerChat.GetChatCustomerProfileDetails(TenantID, Programcode, CustomerID,  UserID,  ClientAPIURL);
        }

        #endregion
    }
}
