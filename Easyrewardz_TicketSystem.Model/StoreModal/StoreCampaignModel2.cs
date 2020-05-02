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

        public string CustomerCount { get; set; }

        public string ChatbotScript { get; set; }
        
        public string SmsScript { get; set; }
        
        public string CampaingPeriod { get; set; }

        public string Status { get; set; }

        public bool SmsFlag { get; set; }

        public bool EmailFlag { get; set; }

        public bool MessengerFlag { get; set; }

        public bool BotFlag { get; set; }
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

    public class StoreCampaignSearchOrder
    {
        public string programCode { get; set; }
        public string mobileNumber { get; set; }
        public string securityToken { get; set; }
    }

    public class CampaignStatusResponse1
    {
        public CustomerpopupDetails useratvdetails { get; set; }
        public StoreCampaignKeyInsight campaignkeyinsight { get; set; }
        public List<StoreCampaignRecommended> campaignrecommended { get; set; }
        public StoreCampaignLastTransactionDetails lasttransactiondetails { get; set; }
    }

    public class CustomerpopupDetails
    {
        public string name { get; set; }
        public string mobileNumber { get; set; }
        public string tiername { get; set; }
        public string lifeTimeValue { get; set; }
        public string visitCount { get; set; }
             
    }

    public class StoreCampaignLogo
    {
        public int Id { get; set; }
        public string name { get; set; }
    }

    public class StoreCampaignKeyInsight
    {
        public string mobileNumber { get; set; }
        public string insightText { get; set; }
    }

    public class StoreCampaignRecommended
    {
        public string mobileNumber { get; set; }
        public string itemCode { get; set; }
        public string category { get; set; }
        public string subCategory { get; set; }
        public string brand { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public string price { get; set; }
        public string url { get; set; }
        public string imageURL { get; set; }
    }
    public class StoreCampaignLastTransactionDetails
    {
        public string billNo { get; set; }
        public string billDate { get; set; }
        public string storeName { get; set; }
        public string amount { get; set; }
        public string itemDetails { get; set; }
    }
}
