using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;
using System.Data;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IStoreSLA
    {
        /// <summary>
        /// Bind Function List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        List<FunctionList> BindFunctionList(int tenantID, string SearchText);

        /// <summary>
        /// Insert Store SLA
        /// </summary>
        /// <param name="SLA"></param>
        /// <returns></returns>
        int InsertStoreSLA(StoreSLAModel SLA);

        /// <summary>
        /// Update Store SLA
        /// </summary>
        /// <param name="SLA"></param>
        /// <returns></returns>
        int UpdateStoreSLA(StoreSLAModel SLA);

        //bool UpdateStoreSLADetails(SLADetail sLADetail, int TenantID, int UserID);
        /// <summary>
        /// Delete Store SLA
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="SLAID"></param>
        /// <returns></returns>
        int DeleteStoreSLA(int tenantID, int SLAID);

        /// <summary>
        /// Store SLA List
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        List<StoreSLAResponseModel> StoreSLAList(int tenantID);

        /// <summary>
        /// Get Store SLA Detail
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="SLAID"></param>
        /// <returns></returns>
        StoreSLAResponseModel GetStoreSLADetail(int TenantID, int SLAID);

        /// <summary>
        /// Store Bulk Upload SLA
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="DataSetCSV"></param>
        /// <returns></returns>
        List<string> StoreBulkUploadSLA(int TenantID, int CreatedBy, DataSet DataSetCSV);
    }
}
