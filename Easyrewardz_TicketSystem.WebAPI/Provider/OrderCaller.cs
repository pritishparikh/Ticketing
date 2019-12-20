using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class OrderCaller
    {
        public IOrder _orderRepository;


        /// <summary>
        /// Get Order By Number
        /// </summary>
        /// <param name="order"></param>
        /// <param name="OrderNumber"></param>
        /// <returns></returns>
        public OrderMaster getOrderDetailsByNumber(IOrder order, string OrderNumber)
        {
            _orderRepository = order;
            return _orderRepository.getOrderbyNumber(OrderNumber);
        }

        /// <summary>
        /// Add OrderDetail
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderMaster"></param>
        /// <returns></returns>
        public int addOrder(IOrder order,OrderMaster orderMaster)
        {
            _orderRepository = order;
            return _orderRepository.addOrderDetails(orderMaster);
        }



    }
}
