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

        int addOrderDetails(OrderMaster orderMaster, int TenantId);


        List<CustomOrderMaster> getOrderListwithItemDetail(string OrderNumber, int TenantID);

         List<CustomOrderDetailsByCustomer> getOrderListByCustomerID(int CustomerID, int TenantID);

    }
}
