using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IMaster
    {
        /// <summary>
        /// GetStoreUserList
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<StoreUser> GetStoreUserList(int TenantId, int UserID);

        /// <summary>
        /// GetStoreFunctionList
        /// </summary>
        /// <param name="TenantId"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<StoreFunctionModel> GetStoreFunctionList(int TenantId, int UserID);

        /// <summary>
        /// GetRegionZoneList
        /// </summary>
        /// <returns></returns>
        List<RegionZoneMaster> GetRegionlist(int UserID);
    }
}
