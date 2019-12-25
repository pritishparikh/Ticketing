﻿using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class Customercaller
    {
        #region Variable
        public ICustomer _customerRepository;
        #endregion

        #region Customer wrapper method

        /// <summary>
        /// Get Customer Detail by id
        /// </summary>
        public CustomerMaster getCustomerDetailsById(ICustomer customer, int CustomerID)
        {
            _customerRepository = customer;
            return _customerRepository.getCustomerbyId(CustomerID);
        }

        /// <summary>
        /// Get Customer Detail by id/Phone
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public List<CustomerMaster> getCustomerDetailsByEmailIdandPhone(ICustomer customer, string SearchText)
        {
            _customerRepository = customer;
            return _customerRepository.getCustomerbyEmailIdandPhoneNo(SearchText);
        }

        /// <summary>
        /// Add Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerMaster"></param>
        /// <returns></returns>
        public int addCustomer(ICustomer customer, CustomerMaster customerMaster)
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

        #endregion

    }
}
