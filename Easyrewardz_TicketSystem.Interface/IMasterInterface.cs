﻿using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Master tables
    /// </summary>
   public interface IMasterInterface
    {
        /// <summary>
        /// Get channel of purchase
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<ChannelOfPurchase> GetChannelOfPurchaseList(int TenantID);

        /// <summary>
        /// Get department list
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<DepartmentMaster> GetDepartmentList(int TenantID);

        /// <summary>
        /// Get function by department
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        List<FuncationMaster> GetFunctionByDepartment(int DepartmentID,int TenantID);

        /// <summary>
        /// Get list of the payment mode
        /// </summary>
        /// <returns></returns>
        List<PaymentMode> GetPaymentMode();

        /// <summary>
        /// Get list of the ticket soruces
        /// </summary>
        /// <returns></returns>
        List<TicketSourceMaster> GetTicketSources();

        /// <summary>
        /// Get SMTP details by Tenant Id
        /// </summary>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        SMTPDetails GetSMTPDetails(int TenantID);
    }
}
