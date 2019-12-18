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

        public List<CustomerMaster> getCustomerDetailsById(ICustomer customer,int CustomerID)
        {
            _customerRepository = customer;
            return _customerRepository.getCustomerbyId(CustomerID);
        }

    }
}
