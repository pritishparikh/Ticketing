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
        public string programCode { get; set; }
        public string mobileNumber { get; set; }
        public string securityToken { get; set; }
    }

    public class StoresCampaignStatusResponse
    {
        public CustomerpopupDetails useratvdetails { get; set; }
        public StoreCampaignKeyInsight campaignkeyinsight { get; set; }
        public List<StoreCampaignRecommended> campaignrecommended { get; set; }
        public StoreCampaignLastTransactionDetails lasttransactiondetails { get; set; }
        public ShareCampaignViaSettingModal ShareCampaignViaSettingModal { get; set; }

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
        public bool ShowKeyInsights { get; set; } = true;
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
        public List<StoreCampaignLastTransactionitemDetailsDetails> itemDetails { get; set; }
    }

    public class StoreCampaignLastTransactionitemDetailsDetails
    {
        public string mobileNo { get; set; }
        public string article { get; set; }
        public string quantity { get; set; }
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

