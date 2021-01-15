using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Model
{
    public class StoreClaimMaster
    {

        /// <summary>
        /// ClaimID
        /// </summary>
        public int ClaimID { get; set; }
        /// <summary>
        /// TenantID
        /// </summary>
        public int TenantID { get; set; }
        /// <summary>
        /// BrandID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// CategoryID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// SubCategoryID
        /// </summary>
        public int SubCategoryID { get; set; }

        /// <summary>
        /// IssueTypeID
        /// </summary>
        public int IssueTypeID { get; set; }

        /// <summary>
        /// ClaimPercent
        /// </summary>
        public double ClaimPercent { get; set; }

        /// <summary>
        /// OrderIDs
        /// </summary>
        public string OrderIDs { get; set; }

        /// <summary>
        /// CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// CustomerID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// OrderMasterID
        /// </summary>
        public int OrderMasterID { get; set; }

        /// <summary>
        /// OrderItemID
        /// </summary>
        public string OrderItemID { get; set; }

        /// <summary>
        /// TicketID
        /// </summary>
        public int TicketID { get; set; }

        /// <summary>
        /// TaskID
        /// </summary>
        public int TaskID { get; set; }

    }
}
