using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class CustomerChatMaster
    {
        /// <summary>
        /// Chat ID of Customer
        /// </summary>
        public int ChatID { get; set; }

        /// <summary>
        /// Store ID 
        /// </summary>
        public string StoreID { get; set; }

        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Customer Name 
        /// </summary>
        public string CumtomerName { get; set; }

        /// <summary>
        /// Customer Mobile No 
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// New Message Count
        /// </summary>
        public int MessageCount { get; set; }

        /// <summary>
        /// Time Ago Chat
        /// </summary>
        public int TimeAgo { get; set; }

        /// <summary>
        /// On going Chat Count
        /// </summary>
        public int OngoingChatCount { get; set; }

        /// <summary>
        /// Chat Status 
        /// </summary>
        public int ChatStatus { get; set; }

        /// <summary>
        /// Created BY
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created date 
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Updated Date
        /// </summary>
        public DateTime UpdatedDate{ get; set; }
    }
}
