using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class GraphCaller
    {
        private IGraph _IGraph;

        public List<User> GetUserList(IGraph _Graph,int TenantID, int UserID)
        {
            _IGraph = _Graph;
            return _IGraph.GetUserList(TenantID, UserID);
        }

        public GraphModal GetGraphCountData(IGraph _Graph, int TenantID, int UserID, GraphCountDataRequest GraphCountData)
        {
            _IGraph = _Graph;
            return _IGraph.GetGraphCountData(TenantID, UserID, GraphCountData);
        }
        public GraphData GetGraphData(IGraph _Graph, int TenantID, int UserID, GraphCountDataRequest GraphCountData)
        {
            _IGraph = _Graph;
            return _IGraph.GetGraphData(TenantID, UserID, GraphCountData);
        }
    }
}
