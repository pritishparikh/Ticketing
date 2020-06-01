using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Interface;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easyrewardz_TicketSystem.WebAPI.Provider
{
    public class HSChatTicketingCaller
    {
        #region Variable
        public IHSChatTicketing hSChatTicketing;
        #endregion

        public List<CustomGetChatTickets> GetTicketsOnLoad(IHSChatTicketing _hSChatTicketing, int statusID, int TenantID, int userMasterID,string programCode)
        {
            hSChatTicketing = _hSChatTicketing;
            return hSChatTicketing.GetTicketsOnLoad(statusID, TenantID, userMasterID, programCode);
        }
    }
}
