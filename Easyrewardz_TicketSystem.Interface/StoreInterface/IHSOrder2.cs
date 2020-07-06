using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IHSOrder
    {
        /// <summary>
        /// GetOrdersDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        OrderResponseDetails GetOrdersDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);

        /// <summary>
        /// GetShoppingBagDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        ShoppingBagResponseDetails GetShoppingBagDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);

        /// <summary>
        /// GetShoppingBagDeliveryType
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        List<ShoppingBagDeliveryFilter> GetShoppingBagDeliveryType(int tenantId, int userId, int pageID);

        /// <summary>
        /// GetShipmentDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        OrderResponseDetails GetShipmentDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);

        /// <summary>
        /// GetOrderTabSettingDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        OrderTabSetting GetOrderTabSettingDetails(int tenantId, int userId);

        /// <summary>
        /// SetOrderHasBeenReturn
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderID"></param>
        /// <param name="Returnby"></param>
        /// <returns></returns>
        int SetOrderHasBeenReturn(int tenantId, int userId, int orderID, string Returnby = "Order");

        /// <summary>
        /// GetOrderShippingTemplate
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shippingTemplateRequest"></param>
        /// <returns></returns>
        ShippingTemplateDetails GetOrderShippingTemplate(int tenantId, int userId, ShippingTemplateRequest shippingTemplateRequest);

        /// <summary>
        /// InsertOrderShippingTemplate
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="addEditShippingTemplate"></param>
        /// <returns></returns>
        int InsertUpdateOrderShippingTemplate(int tenantID, int userID, AddEditShippingTemplate addEditShippingTemplate);

        /// <summary>
        /// GetOrderShippingTemplateName
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        ShippingTemplateDetails GetOrderShippingTemplateName(int tenantId, int userId);
    }
}
