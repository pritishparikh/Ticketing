using System;
using System.Collections.Generic;
using System.Text;

namespace ClientAPIServiceCall
{
    public class LogModel
    {
        public int TenantID { get; set; }
        public string ProgramCode { get; set; }
        public string RequestUrl { get; set; }
        public string ActionName { get; set; }
        public string Method { get; set; }
        public string RequestToken { get; set; }
        public string RequestBody { get; set; }
        public string QueryString { get; set; }
        public string Response { get; set; }
        public DateTime RequestAt { get; set; }
        public DateTime ResponseAt { get; set; }
        public string IPAddress { get; set; }
        public bool IsClientAPI { get; set; }
    }
}
