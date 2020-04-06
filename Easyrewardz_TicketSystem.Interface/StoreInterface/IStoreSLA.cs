using Easyrewardz_TicketSystem.Model;
using Easyrewardz_TicketSystem.Model.StoreModal;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Interface.StoreInterface
{
    public interface IStoreSLA
    {
         List<FunctionList> BindFunctionList(int tenantID, string SearchText);

        int InsertStoreSLA(StoreSLAModel SLA);

        int UpdateStoreSLA(StoreSLAModel SLA);

        //bool UpdateStoreSLADetails(SLADetail sLADetail, int TenantID, int UserID);

        int DeleteStoreSLA(int tenantID, int SLAID);

        List<StoreSLAResponseModel> StoreSLAList(int tenantID);

        StoreSLAResponseModel GetStoreSLADetail(int TenantID, int SLAID);
    }
}
