using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.CustomModel
{

    public class CampaignScriptDetails
    {
        /// <summary>
        /// Campaign ID
        /// </summary>
        public int CampaignID { get; set; }

        /// <summary>
        /// Campaign Name
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// Campaign Name ID
        /// </summary>
        public int CampaignNameID { get; set; }

        /// <summary>
        /// Campaign Script Less
        /// </summary>
        public string CampaignScriptLess { get; set; }

        /// <summary>
        /// Campaign Script
        /// </summary>
        public string CampaignScript { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Modified On
        /// </summary>
        public string ModifiedOn { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Status Name
        /// </summary>
        public string StatusName { get; set; }
    }

    public class CampaignScriptName
    {
        /// <summary>
        /// Campaign Name ID
        /// </summary>
        public int CampaignNameID { get; set; }

        /// <summary>
        /// Campaign Name
        /// </summary>
        public string CampaignName { get; set; }
    }

    public class CampaignScriptRequest
    {
        /// <summary>
        /// Campaign ID
        /// </summary>
        public int CampaignID { get; set; }

        /// <summary>
        /// Campaign Name ID
        /// </summary>
        public string CampaignNameID { get; set; }

        /// <summary>
        /// Campaign Script
        /// </summary>
        public string CampaignScript { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }
    }
}
