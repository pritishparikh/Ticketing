using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class TemplateModel
    {
       
        public int TemplateID { get; set; }
        public string TemplateName { get; set; }
        public int IssueTypeCount { get; set; }
        public string IssueTypeID{ get; set; }
        public string IssueTypeName { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }

        public string TemplateStatus { get; set; }
    }
}
