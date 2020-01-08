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
        //public string TikcketTitle { get; set; }

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
        /// Ticket Source Id
        /// </summary>
        public int TicketSourceID { get; set; }
        /// <summary>
        /// Brand Id
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// Category Id
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Subcategory Id
        /// </summary>
        public string SubCategory { get; set; }
        /// <summary>
        /// Issue Type Id
        /// </summary>
        public string IssueTypeName { get; set; }
        /// <summary>
        /// Priority Id
        /// </summary>
        public string PriortyName  { get; set; }
        /// <summary>
        /// Customer Id
        /// </summary>
        public string CustomerName { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public string AltNumber { get; set; }
        public string CustomerEmailId  { get; set; }
        public string UpdateDate { get; set; }
        public int? OpenTicket { get; set; }
        public int? Totalticket { get; set; }

        public string Username { get; set; }
        /// <summary>
        /// ChannelOfPurchaseID
        /// </summary>
        public string ChannelOfPurchase { get; set; }
        /// <summary>
        /// AssignedID
        /// </summary>
        public int AssignedID { get; set; }

        /// <summary>
        /// TicketActionID
        /// </summary>
        public string TicketActionName { get; set; }

        /// <summary>
        /// IsInstantEscalateToHighLevel
        /// </summary>
        public bool IsInstantEscalateToHighLevel { get; set; }

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
        public string Status { get; set; }

        /// <summary>
        /// IsWantToVisitedStore
        /// </summary>
        public bool IsWantToVisitedStore { get; set; }

        /// <summary>
        /// IsAlreadyVisitedStore
        /// </summary>
        public bool IsAlreadyVisitedStore { get; set; }

        /// <summary>
        /// IsWantToAttachOrder
        /// </summary>
        public bool IsWantToAttachOrder { get; set; }


        /// <summary>
        /// TicketTemplateID
        /// </summary>
        public int TicketTemplateID { get; set; }


        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// ModifiedBy
        /// </summary>
        public int? ModifiedBy { get; set; }

        /// <summary>
        /// ModifiedDate
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Store id foe Comma seperate
        /// </summary>
        public string StoreID { get; set; }

        public string OrderItemID { get; set; }

        /// <summary>
        /// Customer Details(Not Neede for now)
        /// </summary>
    }
}
