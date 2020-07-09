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
        /// <summary>
        /// GetOrdersDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public OrderResponseDetails GetOrdersDetails(IHSOrder order, int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            _OrderRepository = order;
            return _OrderRepository.GetOrdersDetails(tenantId, userId, ordersDataRequest);
        }

        /// <summary>
        /// GetShoppingBagDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public ShoppingBagResponseDetails GetShoppingBagDetails(IHSOrder order, int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            _OrderRepository = order;
            return _OrderRepository.GetShoppingBagDetails(tenantId, userId, ordersDataRequest);
        }

        /// <summary>
        /// GetShoppingBagDeliveryType
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public List<ShoppingBagDeliveryFilter> GetShoppingBagDeliveryType(IHSOrder order, int tenantId, int userId, int pageID)
        {
            _OrderRepository = order;
            return _OrderRepository.GetShoppingBagDeliveryType(tenantId, userId, pageID);
        }

        /// <summary>
        /// GetShipmentDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public OrderResponseDetails GetShipmentDetails(IHSOrder order, int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            _OrderRepository = order;
            return _OrderRepository.GetShipmentDetails(tenantId, userId, ordersDataRequest);
        }

        /// <summary>
        /// GetOrderTabSettingDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public OrderTabSetting GetOrderTabSettingDetails(IHSOrder order, int tenantId, int userId)
        {
            _OrderRepository = order;
            return _OrderRepository.GetOrderTabSettingDetails(tenantId, userId);
        }

        /// <summary>
        /// SetOrderHasBeenReturn
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int SetOrderHasBeenReturn(IHSOrder order, int tenantId, int userId, int orderID)
        {
            _OrderRepository = order;
            return _OrderRepository.SetOrderHasBeenReturn(tenantId, userId, orderID);
        }

        /// <summary>
        /// GetOrderShippingTemplate
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shippingTemplateReques"></param>
        /// <returns></returns>
        public ShippingTemplateDetails GetOrderShippingTemplate(IHSOrder order, int tenantId, int userId, ShippingTemplateRequest shippingTemplateReques)
        {
            _OrderRepository = order;
            return _OrderRepository.GetOrderShippingTemplate(tenantId, userId, shippingTemplateReques);
        }

        /// <summary>
        /// InsertUpdateOrderShippingTemplate
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="addEditShippingTemplate"></param>
        /// <returns></returns>
        public int InsertUpdateOrderShippingTemplate(IHSOrder order, int tenantId, int userId, AddEditShippingTemplate addEditShippingTemplate)
        {
            _OrderRepository = order;
            return _OrderRepository.InsertUpdateOrderShippingTemplate(tenantId, userId, addEditShippingTemplate);
        }

        /// <summary>
        /// GetOrderShippingTemplateName
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ShippingTemplateDetails GetOrderShippingTemplateName(IHSOrder order, int tenantId, int userId)
        {
            _OrderRepository = order;
            return _OrderRepository.GetOrderShippingTemplateName(tenantId, userId);
        }

        /// <summary>
        /// SetOrderHasBeenSelfPickUp
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderID"></param>
        /// <param name="PickupDate"></param>
        /// <param name="PickupTime"></param>
        /// <returns></returns>
        public int SetOrderHasBeenSelfPickUp(IHSOrder order, int tenantId, int userId, OrderSelfPickUp orderSelfPickUp)
        {
            _OrderRepository = order;
            return _OrderRepository.SetOrderHasBeenSelfPickUp(tenantId, userId, orderSelfPickUp);
        }
    }
}
