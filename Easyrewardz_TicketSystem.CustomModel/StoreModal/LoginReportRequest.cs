using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel.StoreModal
{
    public class LoginReportRequest
    {
        public string UserID { get; set; }
        public string Startdate { get; set; }
        public string Enddate { get; set; }
        public int TenantID { get; set; }
    }
    public class LoginReportResponse
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string StoreCode { get; set; }
        public string RegionName { get; set; }
        public string Zone { get; set; }
        public string LoginDate { get; set; }
        public string LoginTime { get; set; }
        public string LogoutTime { get; set; }
        public string TotalWorking { get; set; }
    }
}
