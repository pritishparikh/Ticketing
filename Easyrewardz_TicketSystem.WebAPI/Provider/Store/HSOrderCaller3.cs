﻿using Easyrewardz_TicketSystem.Interface;
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
        public ReturnShipmentDetails InsertShipmentAWB(IHSOrder order, int orderID, string itemIDs,int tenantID,int userID)
        {
            _OrderRepository = order;
            return _OrderRepository.CreateShipmentAWB(orderID, itemIDs, tenantID, userID);
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
    }
}
