using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public partial class HSOrderCaller
    {
        /// <summary>
        /// CreateShipmentAWB
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="itemIDs"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public async Task<ReturnShipmentDetails> InsertShipmentAWB(IHSOrder order, int orderID, string itemIDs,int tenantID,int userID,string clientAPIURL,string programCode, int templateID, string phygitalClientAPIURL, WebBotContentRequest webBotcontentRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.CreateShipmentAWB(orderID, itemIDs, tenantID, userID, clientAPIURL, programCode, templateID, phygitalClientAPIURL, webBotcontentRequest);
        }

        /// <summary>
        /// GetItemDetailByOrderID
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public async Task<OrdersItemDetails> GetItemDetailByOrderID(IHSOrder order, int orderID,int tenantID, int userID)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetItemDetailByOrderID(orderID, tenantID, userID);
        }

        /// <summary>
        /// GetAWBInvoicenoDetails
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<ReturnShipmentDetails>> GetAWBInvoicenoDetails(IHSOrder order, int orderID, int tenantID, int userID)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetAWBInvoicenoDetails(orderID, tenantID, userID);
        }
        /// <summary>
        ///Generate Link
        /// </summary>
        /// <param name="sentPaymentLink"></param>
        /// <param name="clientAPIUrlForGenerateToken"></param>
        /// <param name="clientAPIUrlForGeneratePaymentLink"></param>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="programCode"></param>
        /// <returns></returns>
        public async Task<GenerateLinkResponse> GeneratePaymentLink(IHSOrder order, SentPaymentLink sentPaymentLink, string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, int tenantID, int userID, string programCode, string ClientAPIUrl, HSRequestGenerateToken hSRequestGenerateToken)
        {
            _OrderRepository = order;
            return await _OrderRepository.GenerateLink(sentPaymentLink, clientAPIUrlForGenerateToken, clientAPIUrlForGeneratePaymentLink, tenantID, userID, programCode, ClientAPIUrl, hSRequestGenerateToken);
        }

        /// <summary>
        /// CheckPinCodeForCourierAvailibilty
        /// </summary>
        ///  <param name="HSChkCourierAvailibilty"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        ///  <param name="clientAPIUrl"></param>
        /// <returns></returns>
        public async Task<ResponseCourierAvailibilty> CheckPinCodeForCourierAvailibilty(IHSOrder order, HSChkCourierAvailibilty hSChkCourierAvailibilty, int tenantID, int userID,string clientAPIUrl, string PhygitalClientAPIURL)
        {
            _OrderRepository = order;
            return await _OrderRepository.CheckPinCodeForCourierAvailibilty(hSChkCourierAvailibilty, tenantID, userID, clientAPIUrl, PhygitalClientAPIURL);
        }

        /// <summary>
        ///GetStorePinCodeByUserID
        /// </summary>     
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<string> GetStorePinCodeByUserID(IHSOrder order, int tenantID, int userID)
        {
            _OrderRepository = order;
            return await _OrderRepository.GetStorePinCodeByUserID(tenantID, userID);
        }

        /// <summary>
        /// MakeShipmentAsStoreDelivery
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderID"></param>
        /// <param name="itemIDs"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<ReturnShipmentDetails> MakeShipmentAsStoreDelivery(IHSOrder order, int orderID, string itemIDs, int tenantID, int userID, int templateID)
        {
            _OrderRepository = order;
            return await _OrderRepository.MakeShipmentAsStoreDelivery(orderID, itemIDs, tenantID, userID, templateID);
        }

        /// <summary>
        /// CancelOrder
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderCancelRequest"></param>
        /// <returns></returns>
        public async Task<OrderCancelResponse> CancelOrder(IHSOrder order, OrderCancelRequest orderCancelRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.CancelOrder(orderCancelRequest);
        }
        /// <summary>
        /// GetPODDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public async Task<OrderResponseDetails> PODDetails(IHSOrder order, int tenantId, int userId, OrdersDataRequest ordersDataRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.PODDetails(tenantId, userId, ordersDataRequest);
        }
        /// <summary>
        /// PaymentComment
        /// </summary>
        /// <param name="PaymentCommentModel"></param>
        /// <returns></returns>
        public async Task<int> PaymentComment(IHSOrder order, PaymentCommentModel paymentCommentModel  )
        {
            _OrderRepository = order;
            return await _OrderRepository.PaymentComment(paymentCommentModel);
        }

        /// <summary>
        /// GetPODDetails
        /// </summary>
        /// <param name="order"></param>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        public async Task<List<DownloadReportResponse>> DownloadReport(IHSOrder order, DownloadReportRequest downloadReportRequest)
        {
            _OrderRepository = order;
            return await _OrderRepository.DownloadReport(downloadReportRequest);
        }
    }
}
