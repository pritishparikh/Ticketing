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

        List<string> TicketStatusCount(SearchRequest searchparams);

    }
}
