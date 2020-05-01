using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface ICustomerChat
    {

        List<CustomerChatMessages> GetChatMessageDetails(int tenantId, int ChatID);

        int SaveChatMessages(CustomerChatModel ChatMessageDetails);

        List<CustomItemSearchResponseModel>  ChatItemDetailsSearch(string SearchText);

        int SaveCustomerChatMessageReply(CustomerChatReplyModel ChatReply); 


        List<CustomerChatSuggestionModel>  GetChatSuggestions(string SearchText);

    }
}
