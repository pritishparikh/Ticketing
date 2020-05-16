using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreCampaignModel3
    {
        public List<StoreCampaignSettingModel> CampaignSetting { get; set; }
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
        public int ID { get; set; }
        public string Programcode { get; set; }
       
        public int MaxClickAllowed { get; set; }
        public int EnableClickAfterValue { get; set; }
        public string EnableClickAfterDuration { get; set; }
    }




}
