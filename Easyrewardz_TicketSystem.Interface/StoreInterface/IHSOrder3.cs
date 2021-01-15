using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Interface
{
    public partial interface IHSOrder
    {
        /// <summary>
        /// CreateShipmentAWB
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="itemIDs"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        Task<ReturnShipmentDetails> CreateShipmentAWB(int orderID, string itemIDs, int tenantID, int userID,string clientAPIURL, string programCode, int templateID, string phygitalClientAPIURL, WebBotContentRequest webBotcontentRequest);

        /// <summary>
        /// GetItemDetailByOrderID
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        Task<OrdersItemDetails> GetItemDetailByOrderID(int orderID,int tenantID, int userID);

        /// <summary>
        /// GetAWBInvoicenoDetails
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="itemIDs"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
       Task< List<ReturnShipmentDetails>> GetAWBInvoicenoDetails(int orderID, int tenantID, int userID);

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
        Task<GenerateLinkResponse> GenerateLink(SentPaymentLink sentPaymentLink, string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, int tenantID, int userID, string programCode, string ClientAPIUrl, HSRequestGenerateToken hSRequestGenerateToken);

        /// <summary>
        /// CheckPinCodeForCourierAvailibilty
        /// </summary>
        /// <param name="HSChkCourierAvailibilty"></param>
        ///  <param name="clientAPIUrl"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        Task<ResponseCourierAvailibilty> CheckPinCodeForCourierAvailibilty(HSChkCourierAvailibilty hSChkCourierAvailibilty, int tenantID, int userID, string clientAPIUrl, string phygitalClientAPIURL);

        /// <summary>
        /// GetStorePinCodeByUserID
        /// </summary>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        Task<string> GetStorePinCodeByUserID(int tenantID, int userID);

        /// <summary>
        /// MakeShipmentAsStoreDelivery
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="itemIDs"></param>
        /// <param name="tenantID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<ReturnShipmentDetails> MakeShipmentAsStoreDelivery(int orderID, string itemIDs, int tenantID, int userID, int templateID);
        /// <summary>
        /// CancelOrder
        /// </summary>
        /// <param name="orderCancelRequest"></param>
        /// <returns></returns>
        Task<OrderCancelResponse> CancelOrder(OrderCancelRequest orderCancelRequest);

        /// <summary>
        /// GetPODDetails
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="ordersDataRequest"></param>
        /// <returns></returns>
        Task<OrderResponseDetails> PODDetails(int tenantId, int userId, OrdersDataRequest ordersDataRequest);

        /// <summary>
        /// PaymentComment
        /// </summary>
        /// <param name="PaymentCommentModel"></param>
        /// <returns></returns>
        Task<int> PaymentComment(PaymentCommentModel paymentCommentModel);

        /// <summary>
        /// DownloadReport
        /// </summary>
        /// <param name="DownloadReportRequest"></param>
        /// <returns></returns>
        Task <List<DownloadReportResponse>> DownloadReport(DownloadReportRequest downloadReportRequest);
    }
}
