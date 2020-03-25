using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    /// <summary>
    /// CustomDraftDetails
    /// </summary>
    public class CustomDraftDetails
    {

        /// <summary>
        /// Ticket Id
        /// </summary>
        public Int32 TicketId { get; set; }

        /// <summary>
        /// TikcketTitle
        /// </summary>
        public string TicketTitle { get; set; }
        /// <summary>
        /// TicketDescription
        /// </summary>
        public string  TicketDescription { get; set; }
        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// SubCategoryName
        /// </summary>
        public string SubCategoryName { get; set; }
        /// <summary>
        /// IssueTypeName
        /// </summary>
        public string IssueTypeName { get; set; }
        /// <summary>
        /// CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Assigned ID
        /// </summary>
        public int AssignedID { get; set; }
    }
}
