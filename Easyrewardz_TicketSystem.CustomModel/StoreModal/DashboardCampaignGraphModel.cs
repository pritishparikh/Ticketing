using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel.StoreModal
{
    public class DashboardCampaignGraphModel
    {
        public int CampaignStatusCount { get; set; }
        public int CampaignStatus { get; set; }
        public string CampaignStatusName { get; set; }

    }

    public class CampaignStatusGraphRequest
    {

        public int TenantID { get; set; }
        public string CampaignNameIDs { get; set; }
        public string UserIds { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StoreIDs { get; set; }
        public string ZoneIDs { get; set; }
    }

    public class CampaignNameList
    {

        public int CampaignNameID { get; set; }
        public string CampaignName { get; set; }
        public string CampaignCode { get; set; }
    }
}
