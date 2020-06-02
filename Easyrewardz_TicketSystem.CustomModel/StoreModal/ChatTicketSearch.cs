using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
   public class ChatTicketSearch
    {
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }  
        public int? IssueTypeId { get; set; }
        public int? TicketStatusID { get; set; }
        public int TenantID { get; set; }
        public int UserID { get; set; }
    }
}
