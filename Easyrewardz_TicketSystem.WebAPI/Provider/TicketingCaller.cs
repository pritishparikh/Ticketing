using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class TicketingCaller
    {
        private ITicketing _ticketList;
        public List<TicketingDetails> GetAutoSuggestTicketList(ITicketing _ticket, string TikcketTitle)
        {
            _ticketList = _ticket;
            return _ticketList.GetTicketList(TikcketTitle);
        }

    }
}
