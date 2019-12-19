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
        public ICustomer _customerRepository;

        public CustomerMaster getCustomerDetailsById(ICustomer customer,int CustomerID)
        {
            _customerRepository = customer;
            return _customerRepository.getCustomerbyId(CustomerID);
        }

        public CustomerMaster getCustomerDetailsByEmailIdandPhone(ICustomer customer, string Email, string Phoneno)
        {
            _customerRepository = customer;
            return _customerRepository.getCustomerbyEmailIdandPhoneNo(Email, Phoneno);
        }
        public int addCustomer(ICustomer customer,CustomerMaster customerMaster)
        {
            _customerRepository = customer;
            return _customerRepository.addCustomerDetails(customerMaster);
        }
        public int updateCustomer(ICustomer customer, CustomerMaster customerMaster)
        {
            _customerRepository = customer;
            return _customerRepository.updateCustomerDetails(customerMaster);
        }

    }
}
