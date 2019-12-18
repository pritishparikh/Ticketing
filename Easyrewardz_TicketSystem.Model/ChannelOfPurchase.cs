using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class ChannelOfPurchase
    {
        public int ChannelOfPurchaseID { get; set; }


        public int TenantID { get; set; }


        public string NameOfChannel { get; set; }


        public bool IsActive { get; set; }


        public int CreatedBy { get; set; }


        public DateTime CreatedDate { get; set; }


        public int ModifyBy { get; set; }


        public DateTime ModifiedDate { get; set; }

        public string CreatedByName { get; set; }

    }
}
