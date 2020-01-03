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
        public List<TicketingDetails> GetAutoSuggestTicketList(ITicketing _ticket, string TikcketTitle, int TenantId)
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
        

        public int addTicketDetails(ITicketing _ticket, TicketingDetails ticketingDetails, int TenantId, string FolderPath, string fileName)
        {
            _ticketList = _ticket;
            return _ticketList.addTicket(ticketingDetails, TenantId, FolderPath, fileName);
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
        public int SaveSearch(ITicketing _ticket, int UserID, string SearchParamID, string parameter)
        {
            _ticketList = _ticket;
            return _ticketList.AddSearch(UserID, SearchParamID, parameter);

        }
        #endregion
    }
}

