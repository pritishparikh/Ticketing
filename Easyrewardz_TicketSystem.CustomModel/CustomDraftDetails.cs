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
    }
}
