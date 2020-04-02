using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IItem
    {
        List<string> ItemBulkUpload(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV);
    }
}
