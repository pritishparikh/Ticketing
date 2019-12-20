using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class Customercaller
    {
        #region Variable
        public ICustomer _customerRepository;
        #endregion

        /// <summary>
        /// Get Customer Detail by id
        /// </summary>
        public CustomerMaster getCustomerDetailsById(ICustomer customer,int CustomerID)
        {
            _customerRepository = customer;
            return _customerRepository.getCustomerbyId(CustomerID);
        }

        /// <summary>
        /// Get Customer Detail by id/Phone
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="Email"></param>
        /// <param name="Phoneno"></param>
        /// <returns></returns>
        public CustomerMaster getCustomerDetailsByEmailIdandPhone(ICustomer customer, string Email, string Phoneno)
        {
            _customerRepository = customer;
            return _customerRepository.getCustomerbyEmailIdandPhoneNo(Email, Phoneno);
        }


        /// <summary>
        /// Add Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        public int addCustomer(ICustomer customer,CustomerMaster customerMaster)
        {
            _customerRepository = customer;
            return _customerRepository.addCustomerDetails(customerMaster);
        } 

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        public int updateCustomer(ICustomer customer, CustomerMaster customerMaster)
        {
            _customerRepository = customer;
            return _customerRepository.updateCustomerDetails(customerMaster);
        }

    }
}
