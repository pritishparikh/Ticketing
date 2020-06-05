using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IHSOrder
    {
        OrderResponseDetails GetOrdersDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);
    }
}
