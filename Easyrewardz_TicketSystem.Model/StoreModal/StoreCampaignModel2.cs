using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreCampaignModel2
    {
    
        public int CampaignID { get; set; }

        public string CampaignName { get; set; }

        public string CustomerName { get; set; }
        
        public string ChatbotScript { get; set; }
        
        public string SmsScript { get; set; }
        
        public string CampaingPeriod { get; set; }

        public string Status { get; set; }
    }

    public enum StoreCampaignStatus
    {
        /// <summary>
        /// New
        /// </summary>
        [Description("New")]
        New = 100,

        /// <summary>
        /// InProgress
        /// </summary>
        [Description("InProgress")]
        InProgress = 101,

        /// <summary>
        /// Close
        /// </summary>
        [Description("Close")]
        Close = 102

    }

}
