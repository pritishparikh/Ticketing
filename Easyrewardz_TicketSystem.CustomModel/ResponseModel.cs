using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class ResponseModel
    {

        public string Message { set; get; }
        public bool Status { set; get; }
        public object ResponseData { set; get; }
        public int StatusCode { set; get; }
    }
}
