using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStore
    {
        List<StoreMaster> getStoreDetailByStorecodenPincode(string searchText, int tenantID);
        List<StoreMaster> getStores(string searchText, int tenantID);
        int CreateStore(StoreMaster storeMaster, int TenantID, int UserID);
        int EditStore(StoreMaster storeMaster, int StoreID, int TenantID, int UserID);
        int DeleteStore(int StoreID, int TenantID, int UserID);
        List<CustomStoreList> StoreList(int TenantID);
        List<StoreMaster> SearchStore(int StateID, int PinCode, string Area, bool IsCountry);
        int AttachStore(string StoreId, int TicketId, int CreatedBy);

        /// <summary>
        /// Get list of the store for the selected ticket Id
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        List<StoreMaster> getSelectedStoreByTicketId(int TicketId);

        List<string> BulkUploadStore(int TenantID, int CreatedBy, DataSet DataSetCSV);

    }
}
