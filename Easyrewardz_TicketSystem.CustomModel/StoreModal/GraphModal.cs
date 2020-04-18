namespace Easyrewardz_TicketSystem.CustomModel
{
    public class GraphModal
    {
        public int TaskOpen { get; set; }
        public int TaskDueToday { get; set; }
        public int TaskOverDue { get; set; }
        public int ClaimOpen { get; set; }    
        public int ClaimDueToday { get; set; }
        public int ClaimOverDue { get; set; }
        public int CampaingnOpen { get; set; }
    }

    public class GraphCountDataRequest
    {
        public string UserIds { get; set; }
        public string BrandIDs { get; set; }
        public string DateFrom { get; set; }
        public string DateEnd { get; set; }
    }


    public class GraphData
    {
        public OpenTaskDepartmentWise OpenTaskDepartmentWise { get; set; }
        public TaskByPriority TaskByPriority { get; set; }
        public OpenCampaignByType OpenCampaignByType { get; set; }
        public ClaimVsInvoiceArticle ClaimVsInvoiceArticle { get; set; }
        public OpenClaimStatus OpenClaimStatus { get; set; }
        public ClaimVsInvoiceAmount ClaimVsInvoiceAmount { get; set; }
    }

    public class OpenTaskDepartmentWise
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
    public class TaskByPriority
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
    public class OpenCampaignByType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
    public class ClaimVsInvoiceArticle
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
    public class OpenClaimStatus
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
    public class ClaimVsInvoiceAmount
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
