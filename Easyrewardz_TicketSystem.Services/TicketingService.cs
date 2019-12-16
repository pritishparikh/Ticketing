using Easyrewardz_TicketSystem.DBContext;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Services
{
    public class TicketingService /*: ITicketing*/
    {
        #region Declartion 

        /// <summary>
        /// Context
        /// </summary>
        private readonly ETSContext _eContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public TicketingService(ETSContext context)
        {
            _eContext = context;
        }

        #endregion

        /// <summary>
        /// Getting list of employees
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<TicketingDetails> getTcikets()
        //{
        //    RepositoryService<TicketingDetails> objTicket = new RepositoryService<TicketingDetails>(_eContext);

        //    return objTicket.SelectAll();
        //}
    }
}
