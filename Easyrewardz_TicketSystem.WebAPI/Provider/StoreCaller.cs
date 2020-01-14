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
        public List<StoreMaster> getStoreDetailbyNameAndPincode(IStore store, string SearchText, int tenantID)
        {
            _storeRepository = store;
            return _storeRepository.getStoreDetailByStorecodenPincode(SearchText, tenantID);
        }

        public List<StoreMaster> getStores(IStore store, string searchText, int tenantID)
        {
            _storeRepository = store;
            return _storeRepository.getStores(searchText, tenantID);
        }
        public int AddStore(IStore store, StoreMaster storeMaster, int TenantID, int UserID)
        {
            _storeRepository = store;
            return _storeRepository.CreateStore(storeMaster, TenantID, UserID);
        }
        public int EditStore(IStore store, StoreMaster storeMaster, int StoreID, int TenantID, int UserID)
        {
            _storeRepository = store;
            return _storeRepository.EditStore(storeMaster, StoreID, TenantID, UserID);
        }
        public int DeleteStore(IStore store, int StoreID, int TenantID, int UserID)
        {
            _storeRepository = store;
            return _storeRepository.DeleteStore(StoreID, TenantID, UserID);
        }
    }
}
