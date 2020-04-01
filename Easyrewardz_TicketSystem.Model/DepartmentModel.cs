using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class DepartmentModel
    {
        public int BrandID { get; set; }

        public string BrandName { get; set; }

        public int StoreID { get; set; }

        public string StoreCode { get; set; }

        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public int FunctionID { get; set; }

        public string FunctionName { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class DepartmentDetailsModel
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Status { get; set; }

    }

    public class StoreCodeModel
    {
        public int StoreID { get; set; }
        public int BrandID { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
       
    }

    public class FunctionModel
    {
        public int FunctionID { get; set; }
        public int DepartmentID { get; set; }
        public string FunctionName { get; set; }
    }

}
