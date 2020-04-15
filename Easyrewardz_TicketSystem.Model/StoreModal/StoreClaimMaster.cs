namespace Easyrewardz_TicketSystem.Model
{
    public class StoreClaimMaster
    {
        public int ClaimID { get; set; }
        public int TenantID { get; set; }
        public int BrandID { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int IssueTypeID { get; set; }
        public double ClaimPercent { get; set; }
        public string OrderIDs { get; set; }
        public int CreatedBy { get; set; }
        public int CustomerID { get; set; }
        public int OrderMasterID { get; set; }
        public string OrderItemID { get; set; }

    }
}
