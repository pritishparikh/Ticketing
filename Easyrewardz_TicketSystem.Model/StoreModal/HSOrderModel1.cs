using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class ModuleConfiguration
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Programcode
        /// </summary>
        public string ProgramCode { get; set; }
        /// <summary>
        /// ShoppingBag
        /// </summary>
        public bool ShoppingBag { get; set; }
        /// <summary>
        /// Payment
        /// </summary>
        public bool Payment { get; set; }
        /// <summary>
        /// Shipment
        /// </summary>
        public bool Shipment { get; set; }
    }

    public class OrderConfiguration
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Programcode
        /// </summary>
        public string ProgramCode { get; set; }
        /// <summary>
        /// ShoppingBag
        /// </summary>
        public bool IntegratedSystem { get; set; }
        /// <summary>
        /// Payment
        /// </summary>
        public bool Payment { get; set; }
        /// <summary>
        /// Shipment
        /// </summary>
        public bool Shipment { get; set; }
    }

    public class OrderDelivered
    {
        public int ID { get; set; }

        public string InvoiceNo { get; set; }

        public string CustomerName { get; set; }

        public string MobileNumber { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string StatusName { get; set; }

        public string ActionTypeName { get; set; }

        public List<OrderDeliveredItem> orderDeliveredItems { get; set; }
    }

    public class OrderDeliveredItem
    {
        public int ID { get; set; }

        public string ItemID { get; set; }

        public string ItemName { get; set; }

        public double ItemPrice { get; set; }

        public int Quantity { get; set; }

        public int OrderID { get; set; }
    }

    public class OrderDeliveredFilterRequest
    {
        public string SearchText { get; set; }

        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string FilterStatus { get; set; }
    }

    public class OrderDeliveredDetails
    {
        public List<OrderDelivered> orderDelivereds { get; set; }

        public int TotalCount { get; set; }
    }

    public class OrderStatusFilter
    {
        public int StatusID { get; set; }

        public string StatusName { get; set; }
    }

    public class ShipmentAssigned
    {
        public string AWBNo { get; set; }

        public string CourierPartner { get; set; }

        public string ReferenceNo { get; set; }

        public string StoreName { get; set; }

        public string StaffName { get; set; }

        public string MobileNumber { get; set; }

        public bool IsProceed { get; set; }

        public string ShipmentAWBID { get; set; }
    }

    public class ShipmentAssignedDetails
    {
        public List<ShipmentAssigned> shipmentAssigned { get; set; }

        public int TotalCount { get; set; }
    }

    public class ShipmentAssignedFilterRequest
    {
        public string SearchText { get; set; }

        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public string FilterReferenceNo { get; set; }
    }
}
