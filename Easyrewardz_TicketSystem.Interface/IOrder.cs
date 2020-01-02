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
        OrderMaster getOrderbyNumber(string OrderNumber);

        int addOrderDetails(OrderMaster orderMaster);

        List<CustomOrderMaster> getOrderListwithItemDetail(string OrderNumber, int TenantID);
        
    }
}
