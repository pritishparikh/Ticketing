using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel.StoreModal
{
    public class StoreTaskCampaignComments
    {

        public int CommentID { get; set; }

        public int CampaignCustomerID { get; set; }

        public string Comment { get; set; }

        public int CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public string CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public string ModifiedByName { get; set; }

        public string ModifiedDate { get; set; }
    }
}
