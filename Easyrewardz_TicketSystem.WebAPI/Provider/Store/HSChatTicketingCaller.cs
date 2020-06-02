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
        public GetChatTicketsByID GetTicketsByID(IHSChatTicketing _hSChatTicketing, int ticketID, int tenantID, int userMasterID, string programCode)
        {
            hSChatTicketing = _hSChatTicketing;
            return hSChatTicketing.GetChatTicketsByID(ticketID, tenantID, userMasterID, programCode);
        }
        public int AddChatTicketNotes(IHSChatTicketing _hSChatTicketing, int ticketID, string comment, int userID, int tenantID,string programCode)
        {
            hSChatTicketing = _hSChatTicketing;
            return hSChatTicketing.AddChatTicketNotes(ticketID, comment, userID, tenantID, programCode);
        }
        public List<ChatTicketNotes> GetChatticketNotes(IHSChatTicketing _hSChatTicketing, int ticketID)
        {
            hSChatTicketing = _hSChatTicketing;
            return hSChatTicketing.GetChatticketNotes(ticketID);
        }
        public int SubmitChatTicket(IHSChatTicketing _hSChatTicketing, int ticketID,int statusID,int userID)
        {
            hSChatTicketing = _hSChatTicketing;
            return hSChatTicketing.SubmitChatTicket(ticketID,statusID, userID);
        }
        public List<CustomGetChatTickets> GetChatTicketsOnSearch(IHSChatTicketing _hSChatTicketing, ChatTicketSearch searchModel)
        {
            hSChatTicketing = _hSChatTicketing;
            return hSChatTicketing.GetTicketsOnSearch(searchModel);
        }
    }
}
