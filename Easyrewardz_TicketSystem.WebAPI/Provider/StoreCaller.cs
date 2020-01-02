using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class StoreCaller
    {

        #region Variable
        public IStore _storeRepository;
        #endregion
        public List<StoreMaster> getStoreDetailbyNameAndPincode(IStore store, string SearchText)
        {
            _storeRepository = store;
            return _storeRepository.getStoreDetailByStorecodenPincode(SearchText);
        }

        public List<StoreMaster> getStores(IStore store, string searchText, int tenantID)
        {
            _storeRepository = store;
            return _storeRepository.getStores(searchText, tenantID);
        }
        

    }
}
