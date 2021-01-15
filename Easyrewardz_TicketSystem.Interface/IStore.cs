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
        /// <summary>
        /// get Store Detail By Storecode Pincode
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<StoreMaster> getStoreDetailByStorecodenPincode(string searchText, int tenantID);

        /// <summary>
        /// get Stores
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<StoreMaster> getStores(string searchText, int tenantID);

        /// <summary>
        /// Create Store
        /// </summary>
        /// <param name="storeMaster"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int CreateStore(StoreMaster storeMaster, int TenantID, int UserID);

        /// <summary>
        /// Edit Store
        /// </summary>
        /// <param name="storeMaster"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int EditStore(StoreMaster storeMaster, int TenantID, int UserID);

        /// <summary>
        /// Delete Store
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        int DeleteStore(int StoreID, int TenantID, int UserID);

        /// <summary>
        /// Store List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<CustomStoreList> StoreList(int TenantID);

        /// <summary>
        /// Search Store
        /// </summary>
        /// <param name="StateID"></param>
        /// <param name="PinCode"></param>
        /// <param name="Area"></param>
        /// <param name="IsCountry"></param>
        /// <returns></returns>
        List<StoreMaster> SearchStore(int StateID, int PinCode, string Area, bool IsCountry);

        /// <summary>
        /// Attach Store
        /// </summary>
        /// <param name="StoreId"></param>
        /// <param name="TicketId"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        int AttachStore(string StoreId, int TicketId, int CreatedBy);

        /// <summary>
        /// Get list of the store for the selected ticket Id
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns></returns>
        List<StoreMaster> getSelectedStoreByTicketId(int TicketId);

        /// <summary>
        /// Bulk Upload Store
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> BulkUploadStore(int TenantID, int CreatedBy, DataSet DataSetCSV);

        /// <summary>
        /// Create Campaign Script
        /// </summary>
        /// <param name="campaignScript"></param>
        /// <returns></returns>
        int CreateCampaignScript(CampaignScript campaignScript);

        /// <summary>
        /// Update Claim Attechment Setting
        /// </summary>
        /// <param name="claimAttechment"></param>
        /// <returns></returns>
        int UpdateClaimAttechmentSetting(ClaimAttechment claimAttechment);

        /// <summary>
        /// Bulk Upload User
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="UserFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> BulkUploadUser(int TenantID, int CreatedBy, int UserFor, DataSet DataSetCSV);


    }
}
