using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    /// <summary>
    /// Order
    /// </summary>
    public class OrderCaller
    {
        #region Variable declaration
        public IOrder _orderRepository;
        #endregion

        #region Methods

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

        #endregion

    }
}
