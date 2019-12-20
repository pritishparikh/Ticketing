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
        /// <summary>
        /// Get Auto Suggest Ticket Title
        /// </summary>
        private ITicketing _ticketList;

        /// <summary>
        /// Get Auto Suggest Ticket List
        /// </summary>
        /// <param name="_ticket"></param>
        /// <param name="TikcketTitle"></param>
        /// <returns></returns>
        public List<TicketingDetails> GetAutoSuggestTicketList(ITicketing _ticket, string TikcketTitle)
        {
            _ticketList = _ticket;
            return _ticketList.GetTicketList(TikcketTitle);
        }

    }
}
