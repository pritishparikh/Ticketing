using System;
using System.ComponentModel.DataAnnotations;

namespace Easyrewardz_TicketSystem.Model
{
    public class TicketingDetails
    {
        /// <summary>
        /// Ticket Id
        /// </summary>
        [Key]
        public int Ticket_ID { get; set; }

        /// <summary>
        /// Ticket Title
        /// </summary>
        public string Ticket_title { get; set; }

        /// <summary>
        /// Ticket Description
        /// </summary>
        public string Ticket_description { get; set; }

        /// <summary>
        /// Ticket Notes
        /// </summary>
        public string Ticket_notes { get; set; }
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public int TicketSource_ID { get; set; }
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public int Brand_ID { get; set; }
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public int Category_ID { get; set; }
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public int SubCategory_ID { get; set; } 
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public int IssueType_ID { get; set; }  
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public int Priority_ID { get; set; } 
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public int Customer_ID { get; set; } 
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public int Order_ID { get; set; }
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public DateTime CreatedDate { get; set; }  
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public int UpdatedBy { get; set; } 
        /// <summary>
        /// Ticket Notes
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}
