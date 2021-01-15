using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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
        Task<OrderResponseDetails> GetOrdersDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);

        /// <summary>
        /// GetShoppingBagDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        Task<ShoppingBagResponseDetails> GetShoppingBagDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);

        /// <summary>
        /// GetShoppingBagDeliveryType
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        Task<List<ShoppingBagDeliveryFilter>> GetShoppingBagDeliveryType(int tenantId, int userId, int pageID);

        /// <summary>
        /// GetShipmentDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        Task<OrderResponseDetails> GetShipmentDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);

        /// <summary>
        /// GetOrderTabSettingDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<OrderTabSetting> GetOrderTabSettingDetails(int tenantId, int userId);

        /// <summary>
        /// SetOrderHasBeenReturn
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderID"></param>
        /// <param name="Returnby"></param>
        /// <returns></returns>
        Task<int> SetOrderHasBeenReturn(int tenantId, int userId, int orderID, string Returnby = "Order");

        /// <summary>
        /// GetOrderShippingTemplate
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shippingTemplateRequest"></param>
        /// <returns></returns>
        Task<ShippingTemplateDetails> GetOrderShippingTemplate(int tenantId, int userId, ShippingTemplateRequest shippingTemplateRequest);

        /// <summary>
        /// InsertOrderShippingTemplate
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="addEditShippingTemplate"></param>
        /// <returns></returns>
        Task<int> InsertUpdateOrderShippingTemplate(int tenantID, int userID, AddEditShippingTemplate addEditShippingTemplate);

        /// <summary>
        /// GetOrderShippingTemplateName
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ShippingTemplateDetails> GetOrderShippingTemplateName(int tenantId, int userId);

        /// <summary>
        /// SetOrderHasBeenSelfPickUp
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderID"></param>
        /// <param name="PickupDate"></param>
        /// <param name="PickupTime"></param>
        /// <returns></returns>
        Task<int> SetOrderHasBeenSelfPickUp(int tenantId, int userId, OrderSelfPickUp orderSelfPickUp);

        /// <summary>
        /// GetCourierPartnerFilter
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
       Task< List<string>> GetCourierPartnerFilter(int tenantId, int userId, int pageID);

        /// <summary>
        /// BulkUploadOrderTemplate
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="UserFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        Task<List<string>> BulkUploadOrderTemplate(int tenantID, int createdBy, int userFor, DataSet dataSetCSV);

        /// <summary>
        /// GetOrderCountry
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<OrderCountry>> GetOrderCountry(int tenantId, int userId);

        /// <summary>
        /// InsertModifyOrderCountry
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="modifyOrderCountryRequest"></param>
        /// <returns></returns>
        Task<int> InsertModifyOrderCountry(int tenantId, int userId, ModifyOrderCountryRequest modifyOrderCountryRequest);

        /// <summary>
        /// GetCustAddressDetails
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
       Task<CustAddressDetails> GetCustAddressDetails(int tenantID, int userID, int orderId);

        /// <summary>
        /// GetPrintLabelDetails
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<PrintLabelDetails> GetPrintLabelDetails(int tenantID, int userID, int orderId);

        /// <summary>
        /// GetSelfPickUpOrdersDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="selfPickupOrdersDataRequest"></param>
        /// <returns></returns>
        Task<SelfPickupOrderResponseDetails> GetSelfPickUpOrdersDetails(int tenantId, int userId, SelfPickupOrdersDataRequest selfPickupOrdersDataRequest);

        /// <summary>
        /// UpdateShoppingAvailableQty
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shoppingAvailableQty"></param>
        /// <returns></returns>
        Task<int> UpdateShoppingAvailableQty(int tenantId, int userId, List<ShoppingAvailableQty> shoppingAvailableQty, WebBotContentRequest webBotContentRequest);
    }
}
