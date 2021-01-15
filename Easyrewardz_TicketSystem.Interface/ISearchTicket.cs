using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Ticket Search
    /// </summary>
    public interface ISearchTicket
    {
        /// <summary>
        /// Search Tickets
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        List<SearchResponse> SearchTickets(SearchRequest searchparams);

        /// <summary>
        /// Ticket Status Count
        /// </summary>
        /// <param name="searchparams"></param>
        /// <returns></returns>
        List<TicketStatusModel> TicketStatusCount(SearchRequest searchparams);

        /// <summary>
        /// Get Tickets On Load
        /// </summary>
        /// <param name="HeaderStatus_ID"></param>
        /// <param name="Tenant_ID"></param>
        /// <param name="AssignTo_ID"></param>
        /// <returns></returns>
        List<SearchResponse> GetTicketsOnLoad(int HeaderStatus_ID,int Tenant_ID,int AssignTo_ID);

        /// <summary>
        /// Get Tickets On Search
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        List<SearchResponse> GetTicketsOnSearch(SearchModel searchModel);

        /// <summary>
        /// Get Tickets On Saved Search
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="SearchParamID"></param>
        /// <returns></returns>
        TicketSaveSearch GetTicketsOnSavedSearch(int TenantID,int UserID,int SearchParamID);
    }
}
