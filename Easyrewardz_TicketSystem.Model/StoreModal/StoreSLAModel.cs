using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class StoreSLAModel
    {
        public int TenantID { get; set; }
        public int SlaID { get; set; }
        public string FunctionID { get; set; }
        public int CreatedBy { get; set; }
        public bool isSLAActive { get; set; }
        public List<SLATargetModel> SLATarget { get; set; }
    }

    public class SLATargetModel
    {
        public int SLATargetID { get; set; }
        public int PriorityID { get; set; }
        public int SLABreachPercent { get; set; }
        public int PriorityResolutionValue { get; set; }
        public string PriorityResolutionDuration { get; set; }
    }

    public class StoreSLAResponseModel
    {
        public int SLAID { get; set; }
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }

        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public int StoreID { get; set; }
        public string StoreName { get; set; }

        public string isSLAActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    

        public List<StoreSLATargetResponseModel> SLATarget { get; set; }
    }

    public class StoreSLATargetResponseModel
    {
        public int SLATargetID { get; set; }
        public int PriorityID { get; set; }
        public string PriorityName { get; set; }
        public string SLABreachPercent { get; set; }
        public int PriorityResolution { get; set; } 
        public string PriorityResolutionDuration { get; set; }

    }

    public class FunctionList
    {
        public int FunctionID { get; set; }
        public string FunctionName { get; set; }

        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public int StoreID { get; set; }
        public string StoreName { get; set; }
    }
}
