using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Ticketing
    /// </summary>
    public interface ITicketing
    {
       
        //IEnumerable<TicketingDetails> getTcikets();

        List<TicketingDetails> GetTicketList(string TikcketTitle);

        int addTicket(TicketingDetails ticketingDetails);
    }
}
