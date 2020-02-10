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
        List<SearchResponse> SearchTickets(SearchRequest searchparams);

        List<TicketStatusModel> TicketStatusCount(SearchRequest searchparams);

        List<SearchResponse> GetTicketsOnLoad(int HeaderStatus_ID,int Tenant_ID,int AssignTo_ID);

        List<SearchResponse> GetTicketsOnSearch(SearchModel searchModel);

        List<SearchResponse> GetTicketsOnSavedSearch(int TenantID,int UserID,int SearchParamID);
    }
}
