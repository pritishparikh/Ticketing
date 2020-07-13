using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {
        ChatCustomerProfileModel GetChatCustomerProfileDetails(int TenantId, string ProgramCode, int CustomerID, int UserID, string ClientAPIURL);

        List<CustomerChatProductModel> GetChatCustomerProducts(int TenantId, string ProgramCode, int CustomerID, string MobileNo);
    }
}
