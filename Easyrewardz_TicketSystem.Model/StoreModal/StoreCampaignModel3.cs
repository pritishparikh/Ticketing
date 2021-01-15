using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreCampaignModel3
    {
        //public List<StoreCampaignSettingModel> CampaignSetting { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  StoreCampaignSettingTimer CampaignSettingTimer { get; set; }
    } 


    public class StoreCampaignSettingModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Campaign Name
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// Campaign Code
        /// </summary>
        public string CampaignCode { get; set; }

        /// <summary>
        /// Program code
        /// </summary>
        public string Programcode { get; set; }

        /// <summary>
        /// Sms Flag
        /// </summary>
        public bool SmsFlag { get; set; }

        /// <summary>
        /// Email Flag
        /// </summary>
        public bool EmailFlag { get; set; }

        /// <summary>
        /// Messenger Flag
        /// </summary>
        public bool MessengerFlag { get; set; }

        /// <summary>
        /// Bot Flag
        /// </summary>
        public bool BotFlag { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created On Name
        /// </summary>
        public string CreatedOnName { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public string CreatedOn { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public int ModifiedBy { get; set; }

        /// <summary>
        /// Modified By Name
        /// </summary>
        public string ModifiedByName { get; set; }

        /// <summary>
        /// Modified On
        /// </summary>
        public string ModifiedOn { get; set; }

    }

    public class StoreCampaignSettingTimer
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Programcode
        /// </summary>
        public string Programcode { get; set; }
        /// <summary>
        /// MaxClickAllowed
        /// </summary>
        public int MaxClickAllowed { get; set; }
        /// <summary>
        /// EnableClickAfterValue
        /// </summary>
        public int EnableClickAfterValue { get; set; }
        /// <summary>
        /// EnableClickAfterDuration
        /// </summary>
        public string EnableClickAfterDuration { get; set; }
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
        /// ProviderName
        /// </summary>
        public string ProviderName { get; set; } = "";
        /// <summary>
        /// CampaignAutoAssigned
        /// </summary>
        public bool CampaignAutoAssigned { get; set; }
        /// <summary>
        /// RaiseTicketFlag
        /// </summary>
        public bool RaiseTicketFlag { get; set; }
        /// <summary>
        /// AddCommentFlag
        /// </summary>
        public bool AddCommentFlag { get; set; }

        // <summary>
        /// StoreFlag
        /// </summary>
        public bool StoreFlag { get; set; }
        /// <summary>
        /// ZoneFlag
        /// </summary>
        public bool ZoneFlag { get; set; }

        // <summary>
        /// UserProductivityReport
        /// </summary>
        public bool UserProductivityReport { get; set; }
        /// <summary>
        /// StoreProductivityReport
        /// </summary>
        public bool StoreProductivityReport { get; set; }

    }

    public class StoreBroadcastConfiguration
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Programcode
        /// </summary>
        public string Programcode { get; set; }
        /// <summary>
        /// MaxClickAllowed
        /// </summary>
        public int MaxClickAllowed { get; set; }
        /// <summary>
        /// EnableClickAfterValue
        /// </summary>
        public int EnableClickAfterValue { get; set; }
        /// <summary>
        /// EnableClickAfterDuration
        /// </summary>
        public string EnableClickAfterDuration { get; set; }
        /// <summary>
        /// SmsFlag
        /// </summary>
        public bool SmsFlag { get; set; }
        /// <summary>
        /// EmailFlag
        /// </summary>
        public bool EmailFlag { get; set; }
        /// <summary>
        /// WhatsappFlag
        /// </summary>
        public bool WhatsappFlag { get; set; }
        /// <summary>
        /// ProviderName
        /// </summary>
        public string ProviderName { get; set; } = "";
    }

    public class StoreAppointmentConfiguration
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Programcode
        /// </summary>
        public string Programcode { get; set; }
        /// <summary>
        /// GenerateOTP
        /// </summary>
        public bool GenerateOTP { get; set; }
        /// <summary>
        /// CardQRcode
        /// </summary>
        public bool CardQRcode { get; set; }
        /// <summary>
        /// CardBarcode
        /// </summary>
        public bool CardBarcode { get; set; }
        /// <summary>
        /// OnlyCard
        /// </summary>
        public bool OnlyCard { get; set; }

        /// <summary>
        /// Send Message to Customer Via WhatsApp Incase od Cancellation
        /// </summary>
        public bool ViaWhatsApp { get; set; }



        /// <summary>
        ///         /// Chck if customer last msg within 24 hours
        /// </summary>
        public bool IsMsgWithin24Hrs { get; set; }

        /// <summary>
        /// Message content to be sent Via WhatsApp
        /// </summary>
        public string MessageViaWhatsApp { get; set; }


        /// <summary>
        ///         /// Send Message to Customer Via WhatsApp Incase od Cancellation
        /// </summary>
        public bool ViaSMS { get; set; }

        /// <summary>
        /// Message content to be sent Via SMS
        /// </summary>
        public string MessageViaSMS { get; set; }


    }

    public class Languages
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Language
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }
    }

    public class SelectedLanguages
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// LanguageID
        /// </summary>
        public int LanguageID { get; set; }
        /// <summary>
        /// CreatedOn
        /// </summary>
        public string CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Language
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// CreaterName
        /// </summary>
        public string CreaterName { get; set; }
    }
}
