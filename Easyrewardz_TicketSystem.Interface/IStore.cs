﻿using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IStore
    {
        List<StoreMaster> getStoreDetailByStorecodenPincode(string searchText, int tenantID);
        List<StoreMaster> getStores(string searchText, int tenantID);
        int CreateStore(StoreMaster storeMaster,int TenantID,int UserID);
        int EditStore(StoreMaster storeMaster, int StoreID, int TenantID,int UserID);
        int DeleteStore(int StoreID, int TenantID, int UserID);
        List<CustomStoreList> StoreList(int TenantID);
        List<StoreMaster> SearchStore(int StateID, int PinCode ,string Area,bool IsCountry );
    }
}
