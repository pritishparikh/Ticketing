using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Ticketing
    /// </summary>
    public interface ITicketing
    {
        //IEnumerable<TicketingDetails> getTcikets();
        /// <summary>
        /// Get Ticket List
        /// </summary>
        /// <param name="TikcketTitle"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<TicketTitleDetails> GetTicketList(string TikcketTitle, int TenantId);

        /// <summary>
        /// add Ticket
        /// </summary>
        /// <param name="ticketingDetails"></param>
        /// <param name="TenantId"></param>
        /// <param name="FolderPath"></param>
        /// <param name="finalAttchment"></param>
        /// <returns></returns>
        int addTicket(TicketingDetails ticketingDetails, int TenantId, string FolderPath, string finalAttchment);

        /// <summary>
        /// Get Draft
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<CustomDraftDetails> GetDraft(int UserID, int TenantId);

        /// <summary>
        /// Search Agent
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        /// <param name="DesignationID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<CustomSearchTicketAgent> SearchAgent(string FirstName, string LastName, string Email, int DesignationID, int TenantId);

        /// <summary>
        /// List Saved Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<UserTicketSearchMaster> ListSavedSearch(int UserID);

        /// <summary>
        /// Get Saved Search By ID
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <returns></returns>
        UserTicketSearchMaster GetSavedSearchByID(int SearchParamID);

        /// <summary>
        /// Delete Saved Search
        /// </summary>
        /// <param name="SearchParamID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int DeleteSavedSearch(int SearchParamID, int UserID);

        /// <summary>
        /// Add Search
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="SearchSaveName"></param>
        /// <param name="parameter"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int AddSearch(int UserID, string SearchSaveName, string parameter, int TenantId);

        /// <summary>
        /// Assign Ticket
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="AgentID"></param>
        /// <param name="Remark"></param>
        /// <returns></returns>
        int AssignTicket(string TicketID, int TenantID, int UserID, int AgentID, string Remark);

        /// <summary>
        /// Schedule
        /// </summary>
        /// <param name="scheduleMaster"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int Schedule(ScheduleMaster scheduleMaster, int TenantID, int UserID);

        /// <summary>
        /// get Notes By Ticket Id
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        List<TicketNotes> getNotesByTicketId(int TicketId);

        /// <summary>
        /// submit ticket
        /// </summary>
        /// <param name="customTicketSolvedModel"></param>
        /// <param name="UserID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int submitticket(CustomTicketSolvedModel customTicketSolvedModel, int UserID, int TenantId);

        /// <summary>
        /// get Ticket Details By TicketId
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="TenantID"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        CustomTicketDetail getTicketDetailsByTicketId(int TicketID, int TenantID, string url);

        /// <summary>
        /// Send Mail
        /// </summary>
        /// <param name="sMTPDetails"></param>
        /// <param name="mailTo"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="mailBody"></param>
        /// <param name="informStore"></param>
        /// <param name="storeIDs"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        bool SendMail(SMTPDetails sMTPDetails, string mailTo, string cc, string bcc, string subject, string mailBody, bool informStore, string storeIDs, int TenantID);

        /// <summary>
        /// Get Ticket History
        /// </summary>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        List<CustomTicketHistory> GetTicketHistory(int TicketID);

        /// <summary>
        /// Get Count By Ticket
        /// </summary>
        /// <param name="ticketID"></param>
        /// <returns></returns>
        CustomCountByTicket GetCountByTicket(int ticketID);

        /// <summary>
        /// Ticket Message listing
        /// </summary>
        /// <param name="ticketID"></param>
        /// <param name="TenantID"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        List<TicketMessage> TicketMessagelisting(int ticketID, int TenantID,string url);

        /// <summary>
        /// Get Agent List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="TicketID"></param>
        /// <returns></returns>
        List<CustomSearchTicketAgent> GetAgentList(int TenantID,int TicketID);

        /// <summary>
        /// Comment On Ticket Detail
        /// </summary>
        /// <param name="ticketingMailerQue"></param>
        /// <param name="finalAttchment"></param>
        /// <returns></returns>
        int CommentOnTicketDetail(TicketingMailerQue ticketingMailerQue, string finalAttchment);

        /// <summary>
        /// Get Progress Bar Details
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        ProgressBarDetail GetProgressBarDetails(int TicketID, int TenantID);

        /// <summary>
        /// set ticket assign for follow up
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="FollowUPUserID"></param>
        /// <param name="UserID"></param>
        void setticketassigforfollowup(int TicketID, string FollowUPUserID, int UserID);

        /// <summary>
        /// get tickets for follow up
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        string getticketsforfollowup(int UserID);

        /// <summary>
        /// ticket unassign from follow up
        /// </summary>
        /// <param name="TicketIDs"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        bool ticketunassigfromfollowup(string TicketIDs, int UserID);

        /// <summary>
        /// Update Draft Ticket
        /// </summary>
        /// <param name="ticketingDetails"></param>
        /// <param name="TenantId"></param>
        /// <param name="FolderPath"></param>
        /// <param name="finalAttchment"></param>
        /// <returns></returns>
        int UpdateDraftTicket(TicketingDetails ticketingDetails, int TenantId, string FolderPath, string finalAttchment);
    }
}
