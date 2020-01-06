using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class SearchCaller
    {
        #region variable
        private ISearchTicket _searchList;
        
        #endregion

        #region Methods for Search

        public List<SearchResponse> GetSearchResults(ISearchTicket _search, SearchRequest searchparams)
        {
           
            _searchList = _search;

            return _searchList.SearchTickets(searchparams);

        }

        public List<string> GetStatusCount(ISearchTicket _search, SearchRequest searchparams)
        {

            _searchList = _search;

            return _searchList.TicketStatusCount(searchparams);

        }
        #endregion 
    }
}
