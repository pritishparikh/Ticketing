using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Order
    /// </summary>
    public interface IOrder
    {
        /// <summary>
        /// get Order by Number
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        OrderMaster getOrderbyNumber(string OrderNumber, int TenantId);

        /// <summary>
        /// add Order Details
        /// </summary>
        /// <param name="orderMaster"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        string addOrderDetails(OrderMaster orderMaster, int TenantId);

        /// <summary>
        /// Add Order Item Details
        /// </summary>
        /// <param name="itemMaster"></param>
        /// <param name="TenantId"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        string AddOrderItemDetails(List<OrderItem> itemMaster, int TenantId,int CreatedBy);

        /// <summary>
        /// Get Order List with Item Detail
        /// </summary>
        /// <param name="OrderNumber"></param>
        /// <param name="Customer_ID"></param>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        List<CustomOrderMaster> getOrderListwithItemDetail(string OrderNumber, int Customer_ID, int TenantID, int CreatedBy);

        /// <summary>
        /// Get Order Item Details
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        List<OrderItem> GetOrderItemDetails(int TenantID, OrderMaster orders); //InvoiceDatedate format : yyyy-MM-dd

        /// <summary>
        /// Get Order List By Customer ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        List<CustomOrderDetailsByCustomer> getOrderListByCustomerID(int CustomerID, int TenantID, int CreatedBy);

        /// <summary>
        /// Get Order List By Claim ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="ClaimID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        CustomOrderDetailsByClaim getOrderListByClaimID(int CustomerID, int ClaimID, int TenantID);

        /// <summary>
        /// Search Product
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        List<CustomSearchProduct> SearchProduct(int CustomerID, string productName);

        /// <summary>
        /// Attach Order
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="TicketId"></param>
        /// <param name="CreatedBy"></param>
        /// <returns></returns>
        int AttachOrder(string OrderID, int TicketId, int CreatedBy);

        /// <summary>
        /// Get Order Detail By Ticket ID
        /// </summary>
        /// <param name="TicketID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<CustomOrderMaster> getOrderDetailByTicketID(int TicketID,int TenantID);

    }
}
