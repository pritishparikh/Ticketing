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
        public int Ticket_ID { get; set; }
        /// <summary>
        /// TenantID
        /// </summary>
        public int Tenant_ID { get; set; }

        /// <summary>
        /// Ticket Title
        /// </summary>
        public string Ticket_title { get; set; }

        /// <summary>
        /// Ticket Description
        /// </summary>
        public string Ticket_description { get; set; }

        /// <summary>
        /// TicketSourceID
        /// </summary>
        public int TicketSourceID { get; set; }

        /// <summary>
        /// Ticket Notes
        /// </summary>
        public string Ticket_notes { get; set; }
        /// <summary>
        /// Ticket Source Id
        /// </summary>
        public int TicketSource_ID { get; set; }
        /// <summary>
        /// Brand Id
        /// </summary>
        public int Brand_ID { get; set; }
        /// <summary>
        /// Category Id
        /// </summary>
        public int Category_ID { get; set; }
        /// <summary>
        /// Subcategory Id
        /// </summary>
        public int SubCategory_ID { get; set; } 
        /// <summary>
        /// Issue Type Id
        /// </summary>
        public int IssueType_ID { get; set; }  
        /// <summary>
        /// Priority Id
        /// </summary>
        public int Priority_ID { get; set; } 
        /// <summary>
        /// Customer Id
        /// </summary>
        public int Customer_ID { get; set; }
        /// <summary>
        /// ChannelOfPurchaseID
        /// </summary>
        public int ChannelOfPurchase_ID { get; set; }
        /// <summary>
        /// AssignedID
        /// </summary>
        public int Assigned_ID { get; set; }

        /// <summary>
        /// TicketActionID
        /// </summary>
        public int TicketAction_ID { get; set; }

        /// <summary>
        /// IsInstantEscalateToHighLevel
        /// </summary>
        public bool IsInstantEscalateToHighLevel { get; set; }

        /// <summary>
        /// Order Id
        /// </summary>
        public int Order_ID { get; set; }
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
        public int Status_ID { get; set; }

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
        public int TicketTemplate_ID { get; set; }


        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// ModifiedBy
        /// </summary>
        public int ModifiedBy { get; set; }

        /// <summary>
        /// ModifiedDate
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Store Details
        /// </summary>
        List<StoreMaster> storeMasters { get; set; }

        /// <summary>
        /// Customer Details
        /// </summary>
        List<CustomerMaster> customerMasters { get; set; }

        /// <summary>
        /// Order Details
        /// </summary>

        List<OrderMaster> orderMasters { get; set; }


        /// <summary>
        /// Task Details
        /// </summary>
        List<TaskMaster> taskMasters { get; set; }

    }
}
