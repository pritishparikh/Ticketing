using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface ICategory
    {
        List<Category> GetCategoryList(int TenantID);
    }
}
