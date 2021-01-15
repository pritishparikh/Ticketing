using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class StoreSLAModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SlaID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FunctionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool isSLAActive { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<SLATargetModel> SLATarget { get; set; }
    }

    public class SLATargetModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int SLATargetID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PriorityID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SLABreachPercent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PriorityResolutionValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PriorityResolutionDuration { get; set; }
    }

    public class StoreSLAResponseModel
    {
        /// <summary>
        /// SLA ID
        /// </summary>
        public int SLAID { get; set; }

        /// <summary>
        /// Function ID
        /// </summary>
        public int FunctionID { get; set; }

        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// Brand ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Brand Name
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Department ID
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Store ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Is SLA Active
        /// </summary>
        public string isSLAActive { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Modified Date
        /// </summary>
        public string ModifiedDate { get; set; }

        /// <summary>
        /// SLA Target list
        /// </summary>
        public List<StoreSLATargetResponseModel> SLATarget { get; set; }
    }

    public class StoreSLATargetResponseModel
    {
        /// <summary>
        /// SLA Target ID
        /// </summary>
        public int SLATargetID { get; set; }

        /// <summary>
        /// Priority ID
        /// </summary>
        public int PriorityID { get; set; }

        /// <summary>
        /// Priority Name
        /// </summary>
        public string PriorityName { get; set; }

        /// <summary>
        /// SLA Breach Percent
        /// </summary>
        public string SLABreachPercent { get; set; }

        /// <summary>
        /// Priority Resolution
        /// </summary>
        public int PriorityResolution { get; set; }

        /// <summary>
        /// Priority Resolution Duration
        /// </summary>
        public string PriorityResolutionDuration { get; set; }

    }

    public class FunctionList
    {
        /// <summary>
        /// Function ID
        /// </summary>
        public int FunctionID { get; set; }

        /// <summary>
        /// Function Name
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// Brand ID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Brand Name
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Department ID
        /// </summary>
        public int DepartmentID { get; set; }

        /// <summary>
        /// Department Name
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Store ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }
    }
}
