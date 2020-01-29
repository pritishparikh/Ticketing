using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
   public class UserModel
    {
         public int  UserID { get; set; }
         public int  TenantID { get; set; }
         public int  RoleID { get; set; }
         public int  DesignationID { get; set; }
         public int  ReporteeID { get; set; }
         public int  DepartmentId { get; set; }
         public int  FunctionID { get; set; }
         public int  IsStoreUser { get; set; }
         public string  UserName { get; set; }
         public string MobileNo { get; set; }
         public string EmailID { get; set; }
         public string SecurePassword { get; set; }
         public bool  IsActive { get; set; }
         public bool  IsCopyEscalation { get; set; }
         public bool  IsAssignEscalation { get; set; }
         public int  LanguageId { get; set; }
         public int  CreatedBy { get; set; }
         public DateTime  CreatedDate { get; set; }
         public int  ModifyBy { get; set; }
         public DateTime  ModifiedDate { get; set; }
         public string  FirstName { get; set; }
         public string LastName { get; set; }
        public UserModel()
        {
            IsStoreUser = 1;
        }
    }
}
