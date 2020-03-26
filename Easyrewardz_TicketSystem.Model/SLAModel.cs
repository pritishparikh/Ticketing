using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class SLAModel
    {
        public int TenantID { get; set; }
        public string IssueTypeID { get; set; }
        public int CreatedBy { get; set; }
        public bool isSLAActive { get; set; }
        public int SLAFor { get; set; }
        public List<SLATargetModel> SLATarget { get; set; }

    }

    public class SLATargetModel
    {
        public int PriorityID { get; set; }
        public int SLABreachPercent { get; set; }
        public int PriorityRespondValue { get; set; }
        public string PriorityRespondDuration { get; set; }
        public int PriorityResolutionValue { get; set; }
        public string PriorityResolutionDuration { get; set; }
    }

    public class SLAResponseModel
    {
        public int SLAID { get; set; }
        public int IssueTpeID { get; set; }
        public string  IssueTpeName { get; set; }

        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }



        public string isSLAActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public double totalpages { get; set; }

        public List<SLATargetResponseModel> SLATarget { get; set; }
    }

    public class SLATargetResponseModel
    {
        public int SLATargetID { get; set; }
        public int PriorityID { get; set; }
        public string PriorityName { get; set; }
        public string SLABreachPercent { get; set; }
        public string PriorityRespond { get; set; }
        public string PriorityResolution { get; set; }

    }

    public class IssueTypeList
    {
        public int IssueTypeID { get; set; }
        public string IssueTypeName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string BrandName { get; set; }


    }


    public class SLADetail
    {
        public int SLAId { get; set; }
        public bool IsActive { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string IssueTypeName { get; set; }
        public List<SLATargetDetail> sLATargetDetails { get; set; }
    }

    public class SLATargetDetail
    {
        public int PriorityID { get; set; }
        public string PriorityName { get; set; }
        public bool IsActive { get; set; }
        public int SLATargetID { get; set; }
        public int SLABridgeInPercantage { get; set; }

        public int SLAResponseValue { get; set; }
        public string SLAResponseType { get; set; }

        public int SLAResolveValue { get; set; }
        public string SLAResolveType { get; set; }

    }

}
