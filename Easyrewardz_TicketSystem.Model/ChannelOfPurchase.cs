using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    /// <summary>
    /// Channel of Purchase
    /// </summary>
   public class ChannelOfPurchase
    {
        /// <summary>
        /// Channel of Purchase Id
        /// </summary>
        public int ChannelOfPurchaseID { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Name of the Channel
        /// </summary>
        public string NameOfChannel { get; set; }

        /// <summary>
        /// Active/In active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public int ModifyBy { get; set; }

        /// <summary>
        /// Modified On
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Modified By Name
        /// </summary>
        public string ModifiedByName { get; set; }

    }
}
