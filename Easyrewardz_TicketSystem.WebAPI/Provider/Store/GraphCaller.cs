using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
