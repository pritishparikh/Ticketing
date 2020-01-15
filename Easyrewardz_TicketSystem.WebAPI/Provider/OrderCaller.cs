using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

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
        public OrderMaster getOrderDetailsByNumber(IOrder order, string OrderNumber ,int TenantId)
        {
            _orderRepository = order;
            return _orderRepository.getOrderbyNumber(OrderNumber, TenantId);
        }

        /// <summary>
        /// Add OrderDetail
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderMaster"></param>
        /// <returns></returns>
        public int addOrder(IOrder order,OrderMaster orderMaster,int TenantId)
        {
            _orderRepository = order;
            return _orderRepository.addOrderDetails(orderMaster, TenantId);
        }

        /// <summary>
        /// Get Order with item list
        /// </summary>
        /// <param name="_orderMaster"></param>
        /// <param name="OrderNumber"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        public List<CustomOrderMaster> GetOrderItemList(IOrder _orderMaster, string OrderNumber, int TenantID)
        {
            _orderRepository = _orderMaster;
            return _orderRepository.getOrderListwithItemDetail(OrderNumber, TenantID);

        }
        public List<CustomOrderDetailsByCustomer> GetOrderDetailsByCustomerID(IOrder _orderMaster, int CustomerID, int TenantID)
        {
            _orderRepository = _orderMaster;
            return _orderRepository.getOrderListByCustomerID(CustomerID, TenantID);

        }

        public CustomOrderDetailsByClaim GetOrderListByClaimID(IOrder _orderMaster, int CustomerID, int ClaimID, int TenantID)
        {
            _orderRepository = _orderMaster;
            return _orderRepository.getOrderListByClaimID(CustomerID, ClaimID, TenantID);

        }

        public List<CustomSearchProduct> SearchProduct(IOrder _orderMaster, int CustomerID, string productName)
        {
            _orderRepository = _orderMaster;
            return _orderRepository.SearchProduct(CustomerID, productName);

        }
        #endregion

    }
}
