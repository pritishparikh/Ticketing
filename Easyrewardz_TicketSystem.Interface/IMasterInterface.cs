using Easyrewardz_TicketSystem.Model;
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
        /// Add Department
        /// </summary>
        /// <param name="DepartmentName"></param>
        /// <param name="TenantID"></param>
        /// <returns></returns>
        int AddDepartment(string DepartmentName, int TenantID,int CreatedBy);

        /// <summary>
        /// Add Function
        /// </summary>
        /// <param name="DepartmentID"></param>
        /// <param name="FunctionName"></param>
        /// <returns></returns>
        int Addfunction(int DepartmentID, string FunctionName,int TenantID, int CreatedBy);

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

        /// <summary>
        ///Get State List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<StateMaster> GetStateList();


        /// <summary>
        ///Get City List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<CityMaster> GetCitylist(int StateId);

        /// <summary>
        ///Get Region List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<RegionMaster> GetRegionList();

        /// <summary>
        ///Get StoreType List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<StoreTypeMaster> GetStoreTypeList();
    }
}
