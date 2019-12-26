using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class TicketActionMaster
    {
        public int TicketID { get; set; }
        public int TenantID { get; set; }
        public string TikcketTitle { get; set; }
        public string TicketDescription { get; set; }

        public int TicketSourceID { get; set; }
        public int BrandID { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }


        public int PriorityID { get; set; }

        public int CustomerID { get; set; }
        public int OrderMasterID { get; set; }

        public int IssueTypeID { get; set; }

        public int ChannelOfPurchaseID { get; set; }


        public int AssignedID { get; set; }


        public int TicketActionID { get; set; }
        public bool IsInstantEscalateToHighLevel { get; set; }
        public int StatusID { get; set; }
        public bool IsWantToVisitedStore { get; set; }
        public bool IsAlreadyVisitedStore { get; set; }
        public bool IsWantToAttachOrder { get; set; }
        public int TicketTemplateID { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
