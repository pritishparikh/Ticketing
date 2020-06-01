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
        public List<TicketStatusModel> GetStatusCount(IHSChatTicketing _hSChatTicketing,int tenantID,int userID,string programCode)
        {
            hSChatTicketing = _hSChatTicketing;
             
            return hSChatTicketing.TicketStatusCount(tenantID, userID, programCode);
        }
        public List<Category> GetCategoryList(IHSChatTicketing _hSChatTicketing, int TenantID,int userID,string programCode)
        {
            hSChatTicketing = _hSChatTicketing;
            return hSChatTicketing.GetCategoryList(TenantID, userID, programCode);
        }
        public List<SubCategory> GetChatSubCategoryByCategoryID(IHSChatTicketing _hSChatTicketing, int categoryID)
        {
            hSChatTicketing = _hSChatTicketing;
            return hSChatTicketing.GetSubCategoryByCategoryID(categoryID);
        }

        public List<IssueType> GetIssueTypeList(IHSChatTicketing _hSChatTicketing, int tenantID, int subCategoryID)
        {
            hSChatTicketing = _hSChatTicketing;
            return hSChatTicketing.GetIssueTypeList(tenantID, subCategoryID);
        }

    }
}
