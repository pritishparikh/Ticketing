using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ICustomer
    {
        List<CustomerMaster> getCustomerbyId(int CustomerID);
    }
}
