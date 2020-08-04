using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Easyrewardz_TicketSystem.Model
{
    public class TicketingDetails
    {
        /// <summary>
        /// Ticket Id
        /// </summary>
        [Key]
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
        public int PriorityID { get; set; }
        /// <summary>
        /// Customer Id
        /// </summary>
        public int CustomerID { get; set; }
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
        /// StatusID
        /// </summary>
        public int StatusID { get; set; }

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
        /// Store Details
        /// </summary>
        List<StoreMaster> storeMasters { get; set; }

        /// <summary>
        /// Store id foe Comma seperate
        /// </summary>
        public string StoreID { get; set; }

        public string OrderItemID { get; set; }

        /// <summary>
        /// Customer Details(Not Neede for now)
        /// </summary>
        //List<CustomerMaster> customerMasters { get; set; }

        /// <summary>
        /// Order Details
        /// </summary>

        public List<OrderMaster> orderMasters { get; set; }


        /// <summary>
        /// Task Details
        /// </summary>
        public List<TaskMaster> taskMasters { get; set; }

        /// <summary>
        /// Ticketing Mail 
        /// </summary>
        public List<TicketingMailerQue> ticketingMailerQues { get; set; }

        /// <summary>
        /// IsInforToStore
        /// </summary>
        public bool IsInforToStore { get; set; }


        /// <summary>
        /// IsInforToStore
        /// </summary>
        public bool IsGenFromStoreCamPaign { get; set; } = false;
    }

    public class TicketTitleDetails
    {
        public string TicketTitle { get; set; }
        public string TicketTitleToolTip { get; set; }
    }

}
