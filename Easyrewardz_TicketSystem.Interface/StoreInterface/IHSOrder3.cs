using System;
using System.Collections.Generic;
using System.Text;
using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;

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
        ReturnShipmentDetails CreateShipmentAWB(int orderID, string itemIDs, int tenantID, int userID);

        /// <summary>
        /// GetItemDetailByOrderID
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        OrdersItemDetails GetItemDetailByOrderID(int orderID,int tenantID, int userID);

        /// <summary>
        /// GetAWBInvoicenoDetails
        /// </summary>
        /// <param name="orderID"></param>
        ///  <param name="itemIDs"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        List<ReturnShipmentDetails> GetAWBInvoicenoDetails(int orderID, int tenantID, int userID);

        /// <summary>
        /// Generate payment Link
        /// </summary>
        /// <param name="SentPaymentLink"></param>
        ///  <param name="clientAPIUrlForGenerateToken"></param>
        ///  <param name="clientAPIUrlForGeneratePaymentLink"></param>
        ///  <param name="tenantID"></param>
        ///  <param name="userID"></param>
        /// <returns></returns>
        int GenerateLink(SentPaymentLink sentPaymentLink, string clientAPIUrlForGenerateToken, string clientAPIUrlForGeneratePaymentLink, int tenantID, int userID, string programCode);

    }
}
