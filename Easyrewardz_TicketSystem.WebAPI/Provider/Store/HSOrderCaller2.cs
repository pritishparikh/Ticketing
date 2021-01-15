using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;
using System.Data;
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
        public async Task<OrderResponseDetails> GetOrdersDetails(IHSOrder order, int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrdersDetails(tenantId, userId, ordersDataRequest);
        }

        /// <summary>
        /// GetShoppingBagDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public async Task<ShoppingBagResponseDetails> GetShoppingBagDetails(IHSOrder order, int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetShoppingBagDetails(tenantId, userId, ordersDataRequest);
        }

        /// <summary>
        /// GetShoppingBagDeliveryType
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public async Task<List<ShoppingBagDeliveryFilter>> GetShoppingBagDeliveryType(IHSOrder order, int tenantId, int userId, int pageID)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetShoppingBagDeliveryType(tenantId, userId, pageID);
        }

        /// <summary>
        /// GetShipmentDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public async Task<OrderResponseDetails> GetShipmentDetails(IHSOrder order, int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetShipmentDetails(tenantId, userId, ordersDataRequest);
        }

        /// <summary>
        /// GetOrderTabSettingDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<OrderTabSetting> GetOrderTabSettingDetails(IHSOrder order, int tenantId, int userId)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderTabSettingDetails(tenantId, userId);
        }

        /// <summary>
        /// SetOrderHasBeenReturn
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public async Task<int> SetOrderHasBeenReturn(IHSOrder order, int tenantId, int userId, int orderID)
        {
            _OrderRepository = order;
            return await _OrderRepository.SetOrderHasBeenReturn(tenantId, userId, orderID);
        }

        /// <summary>
        /// GetOrderShippingTemplate
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shippingTemplateReques"></param>
        /// <returns></returns>
        public async Task<ShippingTemplateDetails> GetOrderShippingTemplate(IHSOrder order, int tenantId, int userId, ShippingTemplateRequest shippingTemplateReques)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderShippingTemplate(tenantId, userId, shippingTemplateReques);
        }

        /// <summary>
        /// InsertUpdateOrderShippingTemplate
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="addEditShippingTemplate"></param>
        /// <returns></returns>
        public async Task<int> InsertUpdateOrderShippingTemplate(IHSOrder order, int tenantId, int userId, AddEditShippingTemplate addEditShippingTemplate)
        {
            _OrderRepository = order;
            return await _OrderRepository.InsertUpdateOrderShippingTemplate(tenantId, userId, addEditShippingTemplate);
        }

        /// <summary>
        /// GetOrderShippingTemplateName
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ShippingTemplateDetails> GetOrderShippingTemplateName(IHSOrder order, int tenantId, int userId)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderShippingTemplateName(tenantId, userId);
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
        public async Task<int> SetOrderHasBeenSelfPickUp(IHSOrder order, int tenantId, int userId, OrderSelfPickUp orderSelfPickUp)
        {
            _OrderRepository = order;
            return await _OrderRepository.SetOrderHasBeenSelfPickUp(tenantId, userId, orderSelfPickUp);
        }

        /// <summary>
        /// GetCourierPartnerFilter
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public async Task<List<string>> GetCourierPartnerFilter(IHSOrder order, int tenantId, int userId, int pageID)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetCourierPartnerFilter(tenantId, userId, pageID);
        }

        /// <summary>
        /// BulkUploadOrderTemplate
        /// </summary>
        /// <param name="order"></param>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="UserFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        public async Task<List<string>> BulkUploadOrderTemplate(IHSOrder order, int tenantID, int createdBy, int userFor, DataSet dataSetCSV)
        {
            _OrderRepository = order;
            return await _OrderRepository.BulkUploadOrderTemplate(tenantID, createdBy, userFor, dataSetCSV);
        }

        /// <summary>
        /// GetOrderCountry
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<OrderCountry>> GetOrderCountry(IHSOrder order, int tenantId, int userId)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetOrderCountry(tenantId, userId);
        }

        /// <summary>
        /// InsertModifyOrderCountry
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="modifyOrderCountryRequest"></param>
        /// <returns></returns>
        public async Task<int> InsertModifyOrderCountry(IHSOrder order, int tenantId, int userId, ModifyOrderCountryRequest modifyOrderCountryRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.InsertModifyOrderCountry(tenantId, userId, modifyOrderCountryRequest);
        }

        /// <summary>
        /// GetCustAddressDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<CustAddressDetails> GetCustAddressDetails(IHSOrder order, int tenantID, int userID, int orderId)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetCustAddressDetails(tenantID, userID, orderId);
        }

        /// <summary>
        /// GetPrintLabelDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<PrintLabelDetails> GetPrintLabelDetails(IHSOrder order, int tenantID, int userID, int orderId)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetPrintLabelDetails(tenantID, userID, orderId);
        }

        /// <summary>
        /// GetSelfPickUpOrdersDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public async Task<SelfPickupOrderResponseDetails> GetSelfPickUpOrdersDetails(IHSOrder order, int tenantId, int userId, SelfPickupOrdersDataRequest selfPickupOrdersDataRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetSelfPickUpOrdersDetails(tenantId, userId, selfPickupOrdersDataRequest);
        }

        /// <summary>
        /// UpdateShoppingAvailableQty
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="shoppingAvailableQty"></param>
        /// <returns></returns>
        public async Task<int> UpdateShoppingAvailableQty(IHSOrder order, int tenantId, int userId, List<ShoppingAvailableQty> shoppingAvailableQty, WebBotContentRequest webBotContentRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.UpdateShoppingAvailableQty(tenantId, userId, shoppingAvailableQty, webBotContentRequest);
        }
    }
}
