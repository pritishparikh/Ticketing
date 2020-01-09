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

    }
}
