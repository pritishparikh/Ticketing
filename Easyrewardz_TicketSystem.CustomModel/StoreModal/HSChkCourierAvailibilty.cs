using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class HSChkCourierAvailibilty
    {
        /// <summary>
        /// Pickup postcode
        /// </summary>
        public int Pickup_postcode { get; set; }

        /// <summary>
        /// Delivery postcode
        /// </summary>
        public int Delivery_postcode { get; set; }

        /// <summary>
        /// Cod
        /// </summary>
        public int Cod { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        public decimal Weight { get; set; }
    }
    public class ResponseCourierAvailibilty
    {
        /// <summary>
        /// StatusCode
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Available
        /// </summary>
        public string Available  { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }
}
