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
        List<CustomerMaster> getCustomerbyId(int CustomerID);

        CustomerMaster getCustomerbyEmailIdandPhoneNo(string Email, string Phoneno);

        int addCustomerDetails(CustomerMaster customerMaster);

        int updateCustomerDetails(CustomerMaster customerMaster);
    }
}
