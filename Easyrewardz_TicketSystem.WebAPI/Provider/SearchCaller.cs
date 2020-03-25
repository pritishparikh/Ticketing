using Easyrewardz_TicketSystem.CustomModel;
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

        public List<TicketStatusModel> GetStatusCount(ISearchTicket _search, SearchRequest searchparams)
        {

            _searchList = _search;

            return _searchList.TicketStatusCount(searchparams);

        }

        public List<SearchResponse> GetTicketsOnLoad(ISearchTicket _search, int HeaderStatus_Id,int Tenant_ID, int AssignTo_ID)
        {
            _searchList = _search;
            return _searchList.GetTicketsOnLoad(HeaderStatus_Id,Tenant_ID,AssignTo_ID);
        }

        public List<SearchResponse> GetTicketsOnSearch(ISearchTicket _search, SearchModel searchModel)
        {
            _searchList = _search;
            return _searchList.GetTicketsOnSearch(searchModel);
        }

        public TicketSaveSearch GetTicketsOnSavedSearch(ISearchTicket _search, int TenantID, int UserID, int SearchParamID)
        {
            _searchList = _search;
            return _searchList.GetTicketsOnSavedSearch(TenantID,  UserID, SearchParamID);
        }


        #endregion



    }
}
