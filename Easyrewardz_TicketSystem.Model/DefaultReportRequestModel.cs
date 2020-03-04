using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class DefaultReportRequestModel
    {
        /*public string AssignedTo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int  Status { get; set; }
        public int Source { get; set; }*/

        /// <summary>
        /// Ticket created from
        /// </summary>
        public string Ticket_CreatedFrom { get; set; }

        /// <summary>
        /// Ticket created up to
        /// </summary>
        public string Ticket_CreatedTo { get; set; }

        /// <summary>
        /// Ticket source ids
        /// </summary>
        public string Ticket_SourceIDs { get; set; }

        /// <summary>
        /// Ticket Status Id 
        /// 0 - No use 
        /// -1 - All 
        /// </summary>
        public int Ticket_StatusID { get; set; }

        /// <summary>
        /// Ticket close From date
        /// </summary>
        public string Ticket_CloseFrom { get; set; }

        /// <summary>
        /// Multiple Ticket Status ID
        /// </summary>
        public string Ticket_StatusIDs { get; set; }

        /// <summary>
        /// Ticket close on this day
        /// </summary>
        public string Ticket_CloseTo { get; set; }

        /// <summary>
        /// Ticket assign Ids
        /// </summary>
        public string Ticket_AssignIDs { get; set; }

        /// <summary>
        /// Report Type ID (DefaultReport)
        /// </summary>
        public int ReportTypeID { get; set; }

    }
}
