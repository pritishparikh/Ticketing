using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class GraphModal
    {
        /// <summary>
        /// Task Open
        /// </summary>
        public int TaskOpen { get; set; }

        /// <summary>
        /// Task Due Today
        /// </summary>
        public int TaskDueToday { get; set; }

        /// <summary>
        /// Task Over Due
        /// </summary>
        public int TaskOverDue { get; set; }

        /// <summary>
        /// Claim Open
        /// </summary>
        public int ClaimOpen { get; set; }

        /// <summary>
        /// Claim Due Today
        /// </summary>
        public int ClaimDueToday { get; set; }

        /// <summary>
        /// Claim Over Due
        /// </summary>
        public int ClaimOverDue { get; set; }

        /// <summary>
        /// Campaign Open
        /// </summary>
        public int CampaingnOpen { get; set; }
    }

    public class GraphCountDataRequest
    {
        /// <summary>
        /// User Ids
        /// </summary>
        public string UserIds { get; set; }

        /// <summary>
        /// Brand IDs
        /// </summary>
        public string BrandIDs { get; set; }

        /// <summary>
        /// From Date
        /// </summary>
        public string DateFrom { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public string DateEnd { get; set; }
    }


    public class GraphData
    {
        /// <summary>
        /// Open Task Department Wise
        /// </summary>
        public List<OpenTaskDepartmentWise> OpenTaskDepartmentWise { get; set; }

        /// <summary>
        /// Task By Priority
        /// </summary>
        public List<TaskByPriority> TaskByPriority { get; set; }

        /// <summary>
        /// Open Campaign By Type
        /// </summary>
        public List<OpenCampaignByType> OpenCampaignByType { get; set; }

        /// <summary>
        /// Claim Vs Invoice Article
        /// </summary>
        public List<ClaimVsInvoiceArticle> ClaimVsInvoiceArticle { get; set; }

        /// <summary>
        /// Open Claim Status
        /// </summary>
        public List<OpenClaimStatus> OpenClaimStatus { get; set; }

        /// <summary>
        /// Claim Vs Invoice Amount
        /// </summary>
        public List<ClaimVsInvoiceAmount> ClaimVsInvoiceAmount { get; set; }
    }

    public class OpenTaskDepartmentWise
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Value { get; set; }
    }
    public class TaskByPriority
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Value { get; set; }
    }
    public class OpenCampaignByType
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Value { get; set; }
    }
    public class ClaimVsInvoiceArticle
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Value { get; set; }
    }
    public class OpenClaimStatus
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Value { get; set; }
    }
    public class ClaimVsInvoiceAmount
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Value { get; set; }
    }
}
