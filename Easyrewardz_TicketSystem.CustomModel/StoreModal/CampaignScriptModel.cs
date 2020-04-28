using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CampaignScriptModel
    {

    }

    public class CampaignScriptDetails
    {
       public int CampaignID { get; set; }
       public string CampaignName { get; set; }
        public int CampaignNameID { get; set; }
        public string CampaignScriptLess { get; set; }
       public string CampaignScript { get; set; }
       public string CreatedBy { get; set; }
       public string CreatedOn { get; set; }
       public string ModifiedBy { get; set; }
       public string ModifiedOn { get; set; }
       public bool Status { get; set; }
       public string StatusName { get; set; }
    }

    public class CampaignScriptName
    {
        public int CampaignNameID { get; set; }
        public string CampaignName { get; set; }
    }

    public class CampaignScriptRequest
    {
        public int CampaignID { get; set; }
        public string CampaignNameID { get; set; }
        public string CampaignScript { get; set; }
        public bool Status { get; set; }
    }
}
