using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class CampaignCustomerModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// CampaignScriptID
        /// </summary>
        public int CampaignScriptID { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// CustomerNumber
        /// </summary>
        public string CustomerNumber { get; set; }
        /// <summary>
        /// CustomerEmailID
        /// </summary>
        public string CustomerEmailID { get; set; }
        /// <summary>
        /// DOB
        /// </summary>
        public string DOB { get; set; }
        /// <summary>
        /// CampaignDate
        /// </summary>
        public string CampaignDate { get; set; }
        /// <summary>
        /// ResponseID
        /// </summary>
        public int ResponseID { get; set; }
        /// <summary>
        /// CallRescheduledTo
        /// </summary>
        public string CallRescheduledTo { get; set; }
        /// <summary>
        /// DoesTicketRaised
        /// </summary>
        public int DoesTicketRaised { get; set; }
        /// <summary>
        /// StatusName
        /// </summary>
        public string StatusName { get; set; }
        /// <summary>
        /// StatusID
        /// </summary>
        public int StatusID { get; set; }
        /// <summary>
        /// HSCampaignResponseList
        /// </summary>
        public List<HSCampaignResponse> HSCampaignResponseList { get; set; }
    }

    public class HSCampaignResponse
    {
        /// <summary>
        /// ResponseID
        /// </summary>
        public int ResponseID { get; set; }
        /// <summary>
        /// Response
        /// </summary>
        public string Response { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// StatusName
        /// </summary>
        public string StatusName { get; set; }
    }
}
