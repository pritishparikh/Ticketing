using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class DefaultReportRequestModel
    {
        public string AssignedTo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int  Status { get; set; }
        public int Source { get; set; }
    }
}
