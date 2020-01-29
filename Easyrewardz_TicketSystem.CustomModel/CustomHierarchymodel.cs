using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class CustomHierarchymodel
    {

        public int DesignationID { get; set; } 
        public int TenantID { get; set; }
        public string DesignationName { get; set; }
        public int ReportToDesignation { get; set; }
        public int CreatedBy { get; set; }
        public int IsActive { get; set; }
        public int Deleteflag { get; set; }
        public string ReportTo { get; set; }
        public string Createdbyperson { get; set; }
        public string Updatedbyperson { get; set; }
        public string Status { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime Updateddate { get; set; }
        public string Createdateformat { get; set; }
        public string Updateddateformat { get; set; }
        public int HierarchyFor { get; set; }

        public CustomHierarchymodel()
        {
            HierarchyFor = 1;
        }
    }
}
