using Easyrewardz_TicketSystem.CustomModel;
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
        /// CreateShipmentAWB
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="itemIDs"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public ReturnShipmentDetails InsertShipmentAWB(IHSOrder order, int orderID, string itemIDs,int tenantID,int userID,string clientAPIURL,string ProgramCode)
        {
            _OrderRepository = order;
            return _OrderRepository.CreateShipmentAWB(orderID, itemIDs, tenantID, userID, clientAPIURL, ProgramCode);
        }

        /// <summary>
        /// GetItemDetailByOrderID
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public OrdersItemDetails GetItemDetailByOrderID(IHSOrder order, int orderID,int tenantID, int userID)
        {
            _OrderRepository = order;
            return _OrderRepository.GetItemDetailByOrderID(orderID, tenantID, userID);
        }

        /// <summary>
        /// GetAWBInvoicenoDetails
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        public List<ReturnShipmentDetails>GetAWBInvoicenoDetails(IHSOrder order, int orderID, int tenantID, int userID)
        {
            _OrderRepository = order;
            return _OrderRepository.GetAWBInvoicenoDetails(orderID, tenantID, userID);
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
        public int GeneratePaymentLink(IHSOrder order, SentPaymentLink sentPaymentLink, string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, int tenantID, int userID, string programCode, string ClientAPIUrl, HSRequestGenerateToken hSRequestGenerateToken)
        {
            _OrderRepository = order;
            return _OrderRepository.GenerateLink(sentPaymentLink, clientAPIUrlForGenerateToken, clientAPIUrlForGeneratePaymentLink, tenantID, userID, programCode, ClientAPIUrl, hSRequestGenerateToken);
        }

        /// <summary>
        /// CheckPinCodeForCourierAvailibilty
        /// </summary>
        ///  <param name="HSChkCourierAvailibilty"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        ///  <param name="clientAPIUrl"></param>
        /// <returns></returns>
        public ResponseCourierAvailibilty CheckPinCodeForCourierAvailibilty(IHSOrder order, HSChkCourierAvailibilty hSChkCourierAvailibilty, int tenantID, int userID,string clientAPIUrl)
        {
            _OrderRepository = order;
            return _OrderRepository.CheckPinCodeForCourierAvailibilty(hSChkCourierAvailibilty, tenantID, userID, clientAPIUrl);
        }

        /// <summary>
        ///GetStorePinCodeByUserID
        /// </summary>     
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetStorePinCodeByUserID(IHSOrder order, int tenantID, int userID)
        {
            _OrderRepository = order;
            return _OrderRepository.GetStorePinCodeByUserID(tenantID, userID);
        }
    }
}
