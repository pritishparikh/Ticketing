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
        public int CustomerID { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// CustomerPhoneNumber
        /// </summary>
        public string CustomerPhoneNumber { get; set; }
        /// <summary>
        /// AltNumber
        /// </summary>
        public string AltNumber { get; set; }
        /// <summary>
        /// CustomerEmailId
        /// </summary>
        public string CustomerEmailId { get; set; }
        /// <summary>
        /// UpdateDate
        /// </summary>
        public string UpdateDate { get; set; }
        /// <summary>
        /// OpenTicket
        /// </summary>
        public int? OpenTicket { get; set; }
        /// <summary>
        /// Totalticket
        /// </summary>
        public int? Totalticket { get; set; }
        /// <summary>
        /// TargetClouredate
        /// </summary>
        public DateTime TargetClouredate { get; set; }

        /// <summary>
        /// Username
        /// </summary>
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
        public int TicketActionTypeID { get; set; }

        /// <summary>
        /// OrderMasterID
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

        /// <summary>
        /// StoreID
        /// </summary>
        public string StoreID { get; set; }
        /// <summary>
        /// StoreNames
        /// </summary>
        public string StoreNames { get; set; }
        /// <summary>
        /// ProductID
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// ProductNames
        /// </summary>
        public string ProductNames { get; set; }

        public string OrderItemID { get; set; }
        public List<Store> stores { get; set; }
        public List<Product> products { get; set; }

    }
    public class Store
    {
        public int StoreID { get; set; }
        public string Storename { get; set; }
    }
    public class Product
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
    }
}
