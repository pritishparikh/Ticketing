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

        List<CustomItemSearchResponseModel>  ChatItemDetailsSearch(string ClientAPIURL,string SearchText, string ProgramCode);

        int SaveCustomerChatMessageReply(CustomerChatReplyModel ChatReply); 


        List<CustomerChatSuggestionModel>  GetChatSuggestions(string SearchText);

        int SendRecommendationsToCustomer(int CustomerID, string MobileNo, string ClientAPIURL, int CreatedBy);

        int SendMessageToCustomer(int ChatID, string MobileNo,string ProgramCode,string Message, string ClientAPIURL,int CreatedBy, int InsertChat);


    }
}
