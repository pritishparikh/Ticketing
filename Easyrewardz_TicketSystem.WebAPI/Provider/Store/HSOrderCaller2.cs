using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class HSOrderCaller
    {

        public OrderResponseDetails GetOrdersDetails(IHSOrder order, int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            _OrderRepository = order;
            return _OrderRepository.GetOrdersDetails(tenantId, userId, ordersDataRequest);
        }
    }
}
