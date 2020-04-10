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

        OrderMaster getOrderbyNumber(string OrderNumber, int TenantId);

        string  addOrderDetails(OrderMaster orderMaster, int TenantId);

        string AddOrderItemDetails(List<OrderItem> itemMaster, int TenantId,int CreatedBy);
        

        List<CustomOrderMaster> getOrderListwithItemDetail(string OrderNumber, int Customer_ID, int TenantID, int CreatedBy);


        List<OrderItem> GetOrderItemDetails(int TenantID, OrderMaster orders); //InvoiceDatedate format : yyyy-MM-dd

        List<CustomOrderDetailsByCustomer> getOrderListByCustomerID(int CustomerID, int TenantID, int CreatedBy);

        CustomOrderDetailsByClaim getOrderListByClaimID(int CustomerID, int ClaimID, int TenantID);

        List<CustomSearchProduct> SearchProduct(int CustomerID, string productName);

        int AttachOrder(string OrderID, int TicketId, int CreatedBy);
        List<CustomOrderMaster> getOrderDetailByTicketID(int TicketID,int TenantID);

    }
}
