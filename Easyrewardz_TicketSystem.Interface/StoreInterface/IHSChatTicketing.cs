using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IHSChatTicketing
    {
        /// <summary>
        /// Get Chat Tickets
        /// </summary>
        /// <param name="statusID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userMasterID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        List<CustomGetChatTickets> GetTicketsOnLoad(int statusID, int tenantID, int userMasterID, string programCode);
       
        /// <summary>
        /// Get Chat Ticket Status Count
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        List<TicketStatusModel> TicketStatusCount(int tenantID, int userID, string programCode);

        /// <summary>
        /// Get CategoryList
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        List<Category> GetCategoryList(int tenantID, int userID,string programCode);

        /// <summary>
        /// Get SubCategoryBy CategoryID
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        List<SubCategory> GetSubCategoryByCategoryID(int categoryID);

        /// <summary>
        /// Get IssueType List
        /// </summary>
        ///  <param name="tenantID"></param>
        /// <param name="subCategoryID"></param>
        /// <returns></returns>
        List<IssueType> GetIssueTypeList(int tenantID, int subCategoryID);

        /// <summary>
        /// Get Chat Tickets By ID
        /// </summary>
        /// <param name="ticketID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userMasterID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        GetChatTicketsByID GetChatTicketsByID(int ticketID, int tenantID, int userMasterID, string programCode);

        /// <summary>
        /// Add Chat Ticket Notes
        /// </summary>
        /// <param name="ticketID"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        /// <param name="tenantID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        int AddChatTicketNotes(int ticketID, string comment, int userID, int tenantID, string programCode);

        /// <summary>
        /// Get Chat Ticket Notes
        /// </summary>
        /// <param name="ticketID"></param>
        List<ChatTicketNotes> GetChatticketNotes(int ticketID);

        /// <summary>
        /// Update Chat Ticket Status
        /// </summary>
        /// <param name="ticketID"></param>
        /// <param name="statusID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        int SubmitChatTicket(int ticketID,int statusID, int userID);

        /// <summary>
        /// Get tickets On View Search click
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        List<CustomGetChatTickets> GetTicketsOnSearch(ChatTicketSearch searchModel);

        /// <summary>
        /// Get Chat Ticket History
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        List<CustomTicketHistory> GetChatTickethistory(int ticketID);

        /// <summary>
        /// Create Chat Ticket
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        int CreateChatTicket(CreateChatTickets createChatTickets);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusID"></param>
        /// <param name="tenantID"></param>
        /// <param name="userMasterID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        List<CustomGetChatTickets> GetTicketsByCustomerOnLoad(int statusID, int tenantID, int userMasterID, string programCode);

        List<CustomGetChatTickets> GetTicketsByCustomerOnSearch(ChatTicketSearch searchModel);
    }
}
