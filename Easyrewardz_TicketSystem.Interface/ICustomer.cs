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
        
        CustomerMaster getCustomerbyId(int CustomerID, int TenantId);

        List<CustomerMaster> getCustomerbyEmailIdandPhoneNo(string searchText);

      
        int addCustomerDetails(CustomerMaster customerMaster, int TenantId);

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
