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
}
