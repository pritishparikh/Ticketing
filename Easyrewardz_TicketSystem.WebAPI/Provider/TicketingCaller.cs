﻿using Easyrewardz_TicketSystem.Interface;
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
        public List<CustomSearchTicketAgent> SearchAgent(ITicketing _ticket, string FirstName, string LastName, string Email, int DesignationID, int TenantId)
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

        public int submitticket(ITicketing _ticket, CustomTicketSolvedModel customTicketSolvedModel, int UserID, int TenantId)
        {
            _ticketList = _ticket;
            return _ticketList.submitticket(customTicketSolvedModel, UserID, TenantId);
        }

        public CustomTicketDetail getTicketDetailsByTicketId(ITicketing _ticket, int ticketID, int TenantID, string url)
        {
            _ticketList = _ticket;

            return _ticketList.getTicketDetailsByTicketId(ticketID, TenantID, url);

        }

        public bool SendMail(ITicketing _ticket, SMTPDetails sMTPDetails, string mailTo, string cc, string bcc, string subject, string mailBody, bool informStore, string storeIDs, int TenantID)
        {
            _ticketList = _ticket;

            return _ticketList.SendMail(sMTPDetails, mailTo, cc, bcc, subject, mailBody, informStore, storeIDs, TenantID);


        }

        public List<CustomTicketHistory> getTickethistory(ITicketing _ticket, int ticketID)
        {
            _ticketList = _ticket;
            return _ticketList.GetTicketHistory(ticketID);
        }
        public CustomCountByTicket GetCounts(ITicketing _ticket, int ticketID)
        {
            _ticketList = _ticket;
            return _ticketList.GetCountByTicket(ticketID);
        }

        public List<TicketMessage> TicketMessage(ITicketing _ticket, int ticketID, int TenantID, string url)
        {
            _ticketList = _ticket;
            return _ticketList.TicketMessagelisting(ticketID, TenantID, url);
        }
        public List<CustomSearchTicketAgent> AgentList(ITicketing _ticket, int TenantID,int TicketID)
        {
            _ticketList = _ticket;
            return _ticketList.GetAgentList(TenantID, TicketID);
        }
        public int CommentticketDetail(ITicketing _ticket, TicketingMailerQue ticketingMailerQue, string finalAttchment)
        {
            _ticketList = _ticket;
            return _ticketList.CommentOnTicketDetail(ticketingMailerQue, finalAttchment);
        }

        /// <summary>
        /// Get Progress bar details
        /// </summary>
        /// <param name="_ticket">Interface</param>
        /// <param name="TicketID">Id of the ticket</param>
        /// <param name="TenantID">Id of the tenant</param>
        /// <returns></returns>
        public ProgressBarDetail GetProgressBarDetails(ITicketing _ticket, int TicketID, int TenantID)
        {
            _ticketList = _ticket;
            return _ticketList.GetProgressBarDetails(TicketID, TenantID);
        }

        /// <summary>
        /// Set ticket assign for follow up
        /// </summary>
        /// <param name="_ticket"></param>
        /// <param name="TicketID"></param>
        /// <param name="FollowUPUserID"></param>
        /// <param name="UserID"></param>
        public void setticketassigforfollowup(ITicketing _ticket, int TicketID, string FollowUPUserID, int UserID)
        {
            _ticketList = _ticket;
            _ticketList.setticketassigforfollowup(TicketID, FollowUPUserID, UserID);
        }

        /// <summary>
        /// Get tickets for follow up
        /// </summary>
        /// <param name="_ticket"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string getticketsforfollowup(ITicketing _ticket, int UserID)
        {
            string ticketIDs = "";
            _ticketList = _ticket;
            ticketIDs = _ticketList.getticketsforfollowup(UserID);
            return ticketIDs;
        }

        /// <summary>
        /// Ticket Unassignment from follow up
        /// </summary>
        /// <param name="_ticket"></param>
        /// <param name="TicketIDs"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool ticketunassigfromfollowup(ITicketing _ticket, string TicketIDs, int UserID)
        {
            bool isUpdated = false;
            _ticketList = _ticket;
            isUpdated = _ticketList.ticketunassigfromfollowup(TicketIDs, UserID);
            return isUpdated;
        }

        /// <summary>
        /// Update Ticke tDetails
        /// </summary>
        /// <param name="_ticket"></param>
        /// <param name="ticketingDetails"></param>
        /// <param name="TenantId"></param>
        /// <param name="FolderPath"></param>
        /// <param name="finalAttchment"></param>
        /// <returns></returns>
        public int UpdateDraftTicket(ITicketing _ticket, TicketingDetails ticketingDetails, int TenantId, string FolderPath, string finalAttchment)
        {
            _ticketList = _ticket;
            return _ticketList.UpdateDraftTicket(ticketingDetails, TenantId, FolderPath, finalAttchment);
        }


        #endregion
    }
}

