using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ChatCustomerProfileModel
    {
        /// <summary>
        /// Customer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Custome rName
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Tier
        /// </summary>
        public string CustomerTier { get; set; }


        /// <summary>
        /// Custome rMobile No
        /// </summary>
        public string CustomerMobileNo { get; set; }


        /// <summary>
        ///Customer Email ID
        /// </summary>
        public string CustomerEmailID { get; set; }


        /// <summary>
        /// Customer TotalPoints
        /// </summary>
        public double TotalPoints { get; set; }

        /// <summary>
        ///Customer LifetimeValue
        /// </summary>
        public double LifetimeValue { get; set; }


        /// <summary>
        /// Customer VisitCount
        /// </summary>
        public int VisitCount { get; set; }


        /// <summary>
        /// Customer BillNumber
        /// </summary>
        public string BillNumber { get; set; }


        /// <summary>
        /// Customer BillAmount
        /// </summary>
        public decimal BillAmount { get; set; }


        /// <summary>
        ///Store Name and Address
        /// </summary>
        public string StoreDetails { get; set; }

        /// <summary>
        ///Transaction Date format(dd MMM yyyy)
        /// </summary>
        public string TransactionDate { get; set; }


        /// <summary>
        ///List of Insights of Customer
        /// </summary>
        public List<string> Insights { get; set; }
    }
}
