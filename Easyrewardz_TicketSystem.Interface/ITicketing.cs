using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Ticketing
    /// </summary>
    public interface ITicketing
    {
        //IEnumerable<TicketingDetails> getTcikets();
        List<TicketTitleDetails> GetTicketList(string TikcketTitle, int TenantId);

        int addTicket(TicketingDetails ticketingDetails, int TenantId, string FolderPath, string finalAttchment);

        List<CustomDraftDetails> GetDraft(int UserID, int TenantId);

        List<CustomSearchTicketAgent> SearchAgent(string FirstName, string LastName, string Email, int DesignationID, int TenantId);

        List<UserTicketSearchMaster> ListSavedSearch(int UserID);

        UserTicketSearchMaster GetSavedSearchByID(int SearchParamID);

        int DeleteSavedSearch(int SearchParamID, int UserID);

        int AddSearch(int UserID, string SearchSaveName, string parameter, int TenantId);

        int AssignTicket(string TicketID, int TenantID, int UserID, int AgentID, string Remark);

        int Schedule(ScheduleMaster scheduleMaster, int TenantID, int UserID);

        List<TicketNotes> getNotesByTicketId(int TicketId);
        int submitticket(CustomTicketSolvedModel customTicketSolvedModel, int UserID, int TenantId);

        CustomTicketDetail getTicketDetailsByTicketId(int TicketID, int TenantID,string url);

        bool SendMail(SMTPDetails sMTPDetails, string mailTo, string cc, string bcc, string subject, string mailBody, bool informStore, string storeIDs, int TenantID);
        List<CustomTicketHistory> GetTicketHistory(int TicketID);

        CustomCountByTicket GetCountByTicket(int ticketID);

        List<CustomTicketMessage> TicketMessagelisting(int ticketID,int TenantID);

       List< CustomSearchTicketAgent> GetAgentList( int TenantID);

        int CommentReplyOnTicketDetail(TicketingMailerQue ticketingMailerQue);
        int CommentOnTicketDetail(TicketingMailerQue ticketingMailerQue);
    }
}
