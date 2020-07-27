﻿using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IAppointment
    {
        /// <summary>
        /// Get Store Details By StoreCode
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="programcode"></param>
        /// <param name="storeCode"></param>
        /// <returns></returns>
        StoreDetails GetStoreDetailsByStoreCode(int tenantID, int userID, string programcode, string storeCode);


        List<StoreOperationalDays> GetStoreOperationalDays(int TenantID, string ProgramCode, int UserID);

        List<SlotTemplateModel> GetSlotTemplates(int TenantID, string ProgramCode);

        List<TemplateBasedSlots> GetGeneratedSlots(CreateStoreSlotTemplate Template);
    }
}
