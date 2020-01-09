using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class CustomClaimMaster
    {

        public int TicketClaimID { get; set; }
        public string TaskStatus { get; set; }
        public string ClaimIssueType { get; set; }
        public string Category { get; set; }
        public string RaisedBy { get; set; }
        public DateTime Creation_on  { get; set; }
        public string  Dateformat { get; set; }
        public string AssignName { get; set; }
    }
}
