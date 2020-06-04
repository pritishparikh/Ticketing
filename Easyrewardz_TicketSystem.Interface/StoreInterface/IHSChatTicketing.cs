using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IHSChatTicketing
    {
        List<CustomGetChatTickets> GetTicketsOnLoad(int statusID, int tenantID, int userMasterID, string programCode);

        List<TicketStatusModel> TicketStatusCount(int tenantID, int userID, string programCode);

        List<Category> GetCategoryList(int tenantID, int userID,string programCode);

        List<SubCategory> GetSubCategoryByCategoryID(int categoryID);

        List<IssueType> GetIssueTypeList(int tenantID, int subCategoryID);

        GetChatTicketsByID GetChatTicketsByID(int ticketID, int tenantID, int userMasterID, string programCode);

        int AddChatTicketNotes(int ticketID, string comment, int userID, int tenantID, string programCode);

        List<ChatTicketNotes> GetChatticketNotes(int ticketID);

        int SubmitChatTicket(int ticketID,int statusID, int userID);

        List<CustomGetChatTickets> GetTicketsOnSearch(ChatTicketSearch searchModel);

        List<CustomTicketHistory> GetChatTickethistory(int ticketID);

        int CreateChatTicket(CreateChatTickets createChatTickets);
    }
}
