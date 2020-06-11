using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class HSChkCourierAvailibilty
    {
        public int Pickup_postcode { get; set; }
        public int Delivery_postcode { get; set; }
        public int Cod { get; set; }
        public decimal Weight { get; set; }
    }
    public class ResponseCourierAvailibilty
    {
        public string StatusCode { get; set; }
        public string Available  { get; set; }
        public string Message { get; set; }
    }
}
