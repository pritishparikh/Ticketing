using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class ChatTicketSearch
    {
        /// <summary>
        /// Category Id
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Sub Category Id
        /// </summary>
        public int? SubCategoryId { get; set; }

        /// <summary>
        /// Issue Type Id
        /// </summary>
        public int? IssueTypeId { get; set; }

        /// <summary>
        /// Ticket Status ID
        /// </summary>
        public int? TicketStatusID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// ChatDate
        /// </summary>
        public string ChatLastDate { get; set; }

        /// <summary>
        /// PageNo
        /// </summary>
        public int PageNo { get; set; }
        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }
    }
}
