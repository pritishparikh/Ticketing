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
        public List<TicketingDetails> GetAutoSuggestTicketList(ITicketing _ticket, string TikcketTitle)
        {
            _ticketList = _ticket;
            return _ticketList.GetTicketList(TikcketTitle);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ticket"></param>
        /// <param name="TikcketTitle"></param>
        /// <returns></returns>
        public int addTicketDetails(ITicketing _ticket, TicketingDetails ticketingDetails)
        {
            _ticketList = _ticket;
            return _ticketList.addTicket(ticketingDetails);
        }
        /// <summary>
        /// Get Draft
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public List<CustomDraftDetails> GetDraft(ITicketing _ticket, int UserID)
        {
            _ticketList = _ticket;
            return _ticketList.GetDraft(UserID);

        }
        #endregion
    }
}

