using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreCampaignModel3
    {
        //public List<StoreCampaignSettingModel> CampaignSetting { get; set; }
        public  StoreCampaignSettingTimer CampaignSettingTimer { get; set; }
    } 


    public class StoreCampaignSettingModel
    {
        public int ID { get; set; }

        public string CampaignName { get; set; }

        public string CampaignCode { get; set; }

        public string Programcode { get; set; }


        public bool SmsFlag { get; set; }

        public bool EmailFlag { get; set; }

        public bool MessengerFlag { get; set; }

        public bool BotFlag { get; set; }


        public int CreatedBy { get; set; }

         public string CreatedOnName { get; set; }

        public string CreatedOn { get; set; }


        public int ModifiedBy { get; set; }

        public string ModifiedByName { get; set; }
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

    }




}
