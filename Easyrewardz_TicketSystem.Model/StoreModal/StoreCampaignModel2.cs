using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreCampaignModel2
    {
        /// <summary>
        /// CampaignID
        /// </summary>
        public int CampaignID { get; set; }
        /// <summary>
        /// CampaignName
        /// </summary>
        public string CampaignName { get; set; }
        /// <summary>
        /// CustomerCount
        /// </summary>
        public string CustomerCount { get; set; }
        /// <summary>
        /// ChatbotScript
        /// </summary>
        public string ChatbotScript { get; set; }
        /// <summary>
        /// SmsScript
        /// </summary>
        public string SmsScript { get; set; }
        /// <summary>
        /// CampaingPeriod
        /// </summary>
        public string CampaingPeriod { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// SmsFlag
        /// </summary>
        public bool SmsFlag { get; set; }
        /// <summary>
        /// EmailFlag
        /// </summary>
        public bool EmailFlag { get; set; }
        /// <summary>
        /// MessengerFlag
        /// </summary>
        public bool MessengerFlag { get; set; }
        /// <summary>
        /// BotFlag
        /// </summary>
        public bool BotFlag { get; set; }
        /// <summary>
        /// MaxClickAllowed
        /// </summary>
        public int MaxClickAllowed { get; set; }
        /// <summary>
        /// StoreCode
        /// </summary>
        public string StoreCode { get; set; }
        /// <summary>
        /// CampaignCode
        /// </summary>
        public string CampaignCode { get; set; }

        //public int SmsClickCount { get; set; }

        //public int EmailClickCount { get; set; }

        //public int MessengerClickCount { get; set; }

        //public int BotClickCount { get; set; }


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
        /// <summary>
        /// program Code
        /// </summary>
        public string programCode { get; set; }

        /// <summary>
        /// mobile Number
        /// </summary>
        public string mobileNumber { get; set; }

        /// <summary>
        /// security Token
        /// </summary>
        public string securityToken { get; set; }
    }

    public class StoresCampaignStatusResponse
    {
        /// <summary>
        /// user atv details
        /// </summary>
        public CustomerpopupDetails useratvdetails { get; set; }

        /// <summary>
        /// campaign key insight
        /// </summary>
        public StoreCampaignKeyInsight campaignkeyinsight { get; set; }

        /// <summary>
        /// campaign recommended
        /// </summary>
        public List<StoreCampaignRecommended> campaignrecommended { get; set; }

        /// <summary>
        /// last transaction details
        /// </summary>
        public StoreCampaignLastTransactionDetails lasttransactiondetails { get; set; }

        /// <summary>
        /// Share Campaign Via Setting Modal
        /// </summary>
        public ShareCampaignViaSettingModal ShareCampaignViaSettingModal { get; set; }

    }

    public class CustomerpopupDetails
    {
        /// <summary>
        /// name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// mobile Number
        /// </summary>
        public string mobileNumber { get; set; }

        /// <summary>
        /// tier name
        /// </summary>
        public string tiername { get; set; }

        /// <summary>
        /// life Time Value
        /// </summary>
        public string lifeTimeValue { get; set; }

        /// <summary>
        /// visit Count
        /// </summary>
        public string visitCount { get; set; }

    }

    public class StoreCampaignLogo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string name { get; set; }
    }

    public class StoreCampaignKeyInsight
    {
        /// <summary>
        /// mobile Number
        /// </summary>
        public string mobileNumber { get; set; }

        /// <summary>
        /// insight Text
        /// </summary>
        public string insightText { get; set; }

        /// <summary>
        /// Show Key Insights
        /// </summary>
        public bool ShowKeyInsights { get; set; } = true;
    }

    public class StoreCampaignRecommended
    {
        /// <summary>
        /// mobile Number
        /// </summary>
        public string mobileNumber { get; set; }

        /// <summary>
        /// item Code
        /// </summary>
        public string itemCode { get; set; }

        /// <summary>
        /// category
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// sub Category
        /// </summary>
        public string subCategory { get; set; }

        /// <summary>
        /// brand
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// color
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// size
        /// </summary>
        public string size { get; set; }

        /// <summary>
        /// price
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// imageURL
        /// </summary>
        public string imageURL { get; set; }
    }
    public class StoreCampaignLastTransactionDetails
    {
        /// <summary>
        /// bill No
        /// </summary>
        public string billNo { get; set; }

        /// <summary>
        /// bill Date
        /// </summary>
        public string billDate { get; set; }

        /// <summary>
        /// store Name
        /// </summary>
        public string storeName { get; set; }

        /// <summary>
        /// amount
        /// </summary>
        public string amount { get; set; }

        /// <summary>
        /// item Details
        /// </summary>
        public List<StoreCampaignLastTransactionitemDetailsDetails> itemDetails { get; set; }
    }

    public class StoreCampaignLastTransactionitemDetailsDetails
    {
        /// <summary>
        /// mobile No
        /// </summary>
        public string mobileNo { get; set; }

        /// <summary>
        /// article
        /// </summary>
        public string article { get; set; }

        /// <summary>
        /// quantity
        /// </summary>
        public string quantity { get; set; }

        /// <summary>
        /// amount
        /// </summary>
        public string amount { get; set; }

    }


    public class ShareCampaignViaSettingModal
    {
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// CustomerNumber
        /// </summary>
        public string CustomerNumber { get; set; }
        /// <summary>
        /// SmsFlag
        /// </summary>
        public bool SmsFlag { get; set; }
        /// <summary>
        /// EmailFlag
        /// </summary>
        public bool EmailFlag { get; set; }
        /// <summary>
        /// MessengerFlag
        /// </summary>
        public bool MessengerFlag { get; set; }
        /// <summary>
        /// BotFlag
        /// </summary>
        public bool BotFlag { get; set; }
        /// <summary>
        /// MaxClickAllowed
        /// </summary>
        public int MaxClickAllowed { get; set; }
        /// <summary>
        /// SmsClickCount
        /// </summary>
        public int SmsClickCount { get; set; }
        /// <summary>
        /// EmailClickCount
        /// </summary>
        public int EmailClickCount { get; set; }
        /// <summary>
        /// MessengerClickCount
        /// </summary>
        public int MessengerClickCount { get; set; }
        /// <summary>
        /// BotClickCount
        /// </summary>
        public int BotClickCount { get; set; }
        /// <summary>
        /// SmsClickable
        /// </summary>
        public bool SmsClickable { get; set; }
        /// <summary>
        /// EmailClickable
        /// </summary>
        public bool EmailClickable { get; set; }
        /// <summary>
        /// MessengerClickable
        /// </summary>
        public bool MessengerClickable { get; set; }
        /// <summary>
        /// BotClickable
        /// </summary>
        public bool BotClickable { get; set; }
    }
}

