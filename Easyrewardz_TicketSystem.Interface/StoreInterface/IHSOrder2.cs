using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IHSOrder
    {
        OrderResponseDetails GetOrdersDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);
        ShoppingBagResponseDetails GetShoppingBagDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);
        List<ShoppingBagDeliveryFilter> GetShoppingBagDeliveryType(int tenantId, int userId, int pageID);
        OrderResponseDetails GetShipmentDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);
    }
}
