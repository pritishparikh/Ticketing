using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
