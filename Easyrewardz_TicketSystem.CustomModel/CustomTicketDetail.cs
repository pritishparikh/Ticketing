using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomTicketDetail
    {
        /// <summary>
        /// Ticket Id
        /// </summary>
        public int TicketID { get; set; }
        /// <summary>
        /// TenantID
        /// </summary>
        public int TenantID { get; set; }
        /// <summary>
        /// Ticket Title
        /// </summary>
        public string TicketTitle { get; set; }

        /// <summary>
        /// Ticket Description
        /// </summary>
        public string Ticketdescription { get; set; }


        /// <summary>
        /// Ticket Notes
        /// </summary>
        public string Ticketnotes { get; set; }

        /// <summary>
        /// Brand Id
        /// </summary>
        public int BrandID { get; set; }
        /// <summary>
        /// Category Id
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// Subcategory Id
        /// </summary>
        public int SubCategoryID { get; set; }
        /// <summary>
        /// Issue Type Id
        /// </summary>
        public int IssueTypeID { get; set; }
        /// <summary>
        /// Priority Id
        /// </summary>
        public int PriortyID { get; set; }
        /// <summary>
        /// Customer Id
        /// </summary>
        public string CustomerName { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public string AltNumber { get; set; }
        public string CustomerEmailId { get; set; }
        public string UpdateDate { get; set; }
        public int? OpenTicket { get; set; }
        public int? Totalticket { get; set; }
        public DateTime TargetClouredate { get; set; }

        public string Username { get; set; }
        /// <summary>
        /// ChannelOfPurchaseID
        /// </summary>
        public int ChannelOfPurchaseID { get; set; }
        /// <summary>
        /// AssignedID
        /// </summary>
        public int AssignedID { get; set; }

        /// <summary>
        /// TicketActionID
        /// </summary>
        public int TicketActionID { get; set; }

        /// <summary>
        /// Order Id
        /// </summary>
        public int OrderMasterID { get; set; }
        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Updated By
        /// </summary>
        public int UpdatedBy { get; set; }
        /// <summary>
        /// Updated Date
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }

        /// </summary>
        public string StoreID { get; set; }

        public string OrderItemID { get; set; }
    }
}
