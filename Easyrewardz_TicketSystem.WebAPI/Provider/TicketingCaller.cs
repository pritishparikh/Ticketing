using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easyrewardz_TicketSystem.CustomModel;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class TicketingCaller
    {
        #region Variable declaration

        /// <summary>
        /// Get Auto Suggest Ticket Title
        /// </summary>
        private ITicketing _ticketList;

        #endregion

        #region Methods 
        /// <summary>
        /// Get Auto Suggest Ticket List
        /// </summary>
        /// <param name="_ticket">Interface</param>
        /// <param name="TikcketTitle">Title of the ticket</param>
        /// <returns></returns>
        public List<TicketTitleDetails> GetAutoSuggestTicketList(ITicketing _ticket, string TikcketTitle, int TenantId)
        {
            _ticketList = _ticket;
            return _ticketList.GetTicketList(TikcketTitle, TenantId); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ticket"></param>
        /// <param name="TikcketTitle"></param>
        /// <returns></returns>
         

        public int addTicketDetails(ITicketing _ticket, TicketingDetails ticketingDetails, int TenantId, string FolderPath, string finalAttchment)
        {
            _ticketList = _ticket;
            return _ticketList.addTicket(ticketingDetails, TenantId, FolderPath, finalAttchment);
        }
        /// <summary>
        /// Get Draft
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CustomDraftDetails> GetDraft(ITicketing _ticket, int UserID, int TenantId)
        {
            _ticketList = _ticket;
            return _ticketList.GetDraft(UserID, TenantId);

        }
        /// <summary>
        /// Search Ticket Agent
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Email"></param>
        /// <param name="DesignationID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CustomSearchTicketAgent> SearchAgent(ITicketing _ticket, string FirstName, string LastName, string Email, int DesignationID,int TenantId)
        {
            _ticketList = _ticket;
            return _ticketList.SearchAgent(FirstName, LastName, Email, DesignationID, TenantId);

        }
        public List<UserTicketSearchMaster> ListSavedSearch(ITicketing _ticket, int UserID)
        {
            _ticketList = _ticket;
            return _ticketList.ListSavedSearch(UserID);

        }
        public UserTicketSearchMaster SavedSearchByID(ITicketing _ticket, int SearchID)
        {
            _ticketList = _ticket;
            return _ticketList.GetSavedSearchByID(SearchID);

        }
        public int DeleteSavedSearch(ITicketing _ticket, int SearchParamID, int UserID)
        {
            _ticketList = _ticket;
            return _ticketList.DeleteSavedSearch(SearchParamID, UserID);

        }
        public int SaveSearch(ITicketing _ticket, int UserID, string SearchParamID, string parameter, int TenantId)
        {
            _ticketList = _ticket;
            return _ticketList.AddSearch(UserID, SearchParamID, parameter, TenantId);

        }
        public int AssignTicket(ITicketing _ticket, string TicketID, int TenantID, int UserID, int AgentID, string Remark)
        {
            _ticketList = _ticket;
            return _ticketList.AssignTicket(TicketID, TenantID, UserID, AgentID, Remark);

        }
        public int Schedule(ITicketing _ticket, ScheduleMaster scheduleMaster, int TenantID, int UserID)
        {
            _ticketList = _ticket;
            return _ticketList.Schedule(scheduleMaster, TenantID, UserID);

        }

        /// <summary>
        /// Get Ticket Notes
        /// </summary>
        /// <param name="_ticket">Interface</param>
        /// <param name="TikcketTitle">Title of the ticket</param>
        /// <returns></returns>
        public List<TicketNotes> getNotesByTicketId(ITicketing _ticket, int TicketId)
        {
            _ticketList = _ticket;
            return _ticketList.getNotesByTicketId(TicketId);
        }

        public int submitticket(ITicketing _ticket, int TicketID, int status, int UserID, int TenantId)
        {
            _ticketList = _ticket;
            return _ticketList.submitticket(TicketID, status, UserID, TenantId);
        }

        public CustomTicketDetail getTicketDetailsByTicketId(ITicketing _ticket, int ticketID, int TenantID)
        {
            _ticketList = _ticket;

            return _ticketList.getTicketDetailsByTicketId(ticketID, TenantID);

        }

        public bool SendMail(ITicketing _ticket, SMTPDetails sMTPDetails, string mailTo, string cc, string bcc, string subject, string mailBody, bool informStore,string storeIDs,int TenantID)
        {
            _ticketList = _ticket;

            return _ticketList.SendMail(sMTPDetails, mailTo, cc, bcc,subject, mailBody, informStore, storeIDs, TenantID);


        }
        
        public List<CustomTicketHistory> getTickethistory(ITicketing _ticket,int ticketID)
        {
            _ticketList = _ticket;
            return _ticketList.GetTicketHistory(ticketID); 
        }

        public DashBoardDataModel GetDashBoardCountData(ITicketing _ticket, string BrandID, string UserID,string fromdate,string todate, int TenantID)
        {
            _ticketList = _ticket;

            return _ticketList.GetDashBoardCountData(BrandID,UserID, fromdate, todate, TenantID);
        }

        public DashBoardGraphModel GetDashBoardGraphdata(ITicketing _ticket,string  BrandID, string UserID, string fromdate, string todate, int TenantID)
        {
            _ticketList = _ticket;

            return _ticketList.GetDashBoardGraphdata(BrandID, UserID, fromdate, todate, TenantID);
        }

        #endregion
    }
}

