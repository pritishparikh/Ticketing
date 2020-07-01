using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    /// <summary>
    /// Interface for the Customer
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        /// get Customer by Id
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        CustomerMaster getCustomerbyId(int CustomerID, int TenantId);

        /// <summary>
        /// get Customer by EmailId and Phone No
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="TenantId"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<CustomerMaster> getCustomerbyEmailIdandPhoneNo(string searchText, int TenantId, int UserID);

        /// <summary>
        /// Add Customer Details
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int addCustomerDetails(CustomerMaster customerMaster, int TenantId);

        /// <summary>
        /// Update Customer Details
        /// </summary>
        /// <param name="customerMaster"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        int updateCustomerDetails(CustomerMaster customerMaster, int TenantId);


        /// <summary>
        /// validate Customer Exist
        /// </summary>
        /// <param name="Cust_EmailId"></param>
        /// <param name="Cust_PhoneNumber"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        string validateCustomerExist(string Cust_EmailId, string Cust_PhoneNumber, int TenantId);
    }
}
