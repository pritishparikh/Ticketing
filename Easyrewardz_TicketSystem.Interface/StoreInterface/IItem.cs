using Easyrewardz_TicketSystem.CustomModel;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IItem
    {
        /// <summary>
        /// ItemBulkUpload
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="CategoryFor"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> ItemBulkUpload(int TenantID, int CreatedBy, int CategoryFor, DataSet DataSetCSV);

        /// <summary>
        /// GetItemList
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        List<ItemModel> GetItemList(int TenantId);
    }
}
