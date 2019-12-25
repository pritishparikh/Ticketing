using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class Template
    {
      
      public int TemplateID { get; set; } 
      public int TenantID { get; set; }
      public int IssueTypeID { get; set; }
      public string TemplateName { get; set; }
      public string TemplateSubject { get; set; }
      public string TemplateBody { get; set; }
      public bool  IsActive { get; set; }
      public int?  CreatedBy { get; set; }
      public DateTime? CreatedDate { get; set; }
      public int? ModifyBy { get; set; }
      public DateTime? ModifiedDate { get; set; }
       
    }
}
