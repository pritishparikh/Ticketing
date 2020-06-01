using Easyrewardz_TicketSystem.CustomModel;
using Easyrewardz_TicketSystem.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Interface
{
    public interface IHSChatTicketing
    {
        List<CustomGetChatTickets> GetTicketsOnLoad(int statusID, int tenantID, int userMasterID, string programCode);

        List<TicketStatusModel> TicketStatusCount(int tenantID, int userID, string programCode);

        List<Category> GetCategoryList(int tenantID, int userID,string programCode);

        List<SubCategory> GetSubCategoryByCategoryID(int categoryID);

        List<IssueType> GetIssueTypeList(int tenantID, int subCategoryID);
    }
}
