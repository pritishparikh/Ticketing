using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.CustomModel.StoreModal;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IGraph
    {
        /// <summary>
        /// GetUserList
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<User> GetUserList(int TenantID, int UserID);

        /// <summary>
        /// GetGraphCountData
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="GraphCountData"></param>
        /// <returns></returns>
        GraphModal GetGraphCountData(int TenantID, int UserID, GraphCountDataRequest GraphCountData);

        /// <summary>
        /// GetGraphData
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="UserID"></param>
        /// <param name="GraphCountData"></param>
        /// <returns></returns>
        GraphData GetGraphData(int TenantID, int UserID, GraphCountDataRequest GraphCountData);

        /// <summary>
        /// Get CampaignName List
        /// </summary>
        /// <param name="TenantID"></param>
        /// <param name="ProgramCode"></param>
        /// <returns></returns>
        Task<List<CampaignNameList>> GetCampaignNameList(int TenantID, string ProgramCode);


        /// <summary>
        /// Dashboard Campaign Graph
        /// </summary>
        /// <param name="CampaignGraphRequest"></param>
        /// <returns></returns>
        Task<List<DashboardCampaignGraphModel>> DashboardCampaignGraph(CampaignStatusGraphRequest CampaignGraphRequest);
    }
}
