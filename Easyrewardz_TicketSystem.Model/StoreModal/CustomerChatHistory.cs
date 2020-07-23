using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
  public class CustomerChatHistory
    {
        /// <summary>
        /// Chat ID of Customer
        /// </summary>
        public int ChatID { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Chat Timing
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

    }
    public class GetChatHistoryModel
    {
        /// <summary>
        /// max Page Size
        /// </summary>
        const int maxPageSize = 20;

        /// <summary>
        /// Chat Id
        /// </summary>
        public int ChatId { get; set; }

        /// <summary>
        /// page Number
        /// </summary>
        public int pageNumber { get; set; } = 1;

        /// <summary>
        /// page Size
        /// </summary>
        public int _pageSize { get; set; } = 10;

        public int pageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }


    public class ChatSessionModel
    {
        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Chat Session Value
        /// </summary>
        public int ChatSessionValue { get; set; }

        /// <summary>
        /// Chat Session Duration
        /// </summary>
        public string ChatSessionDuration { get; set; }

        /// <summary>
        /// Chat Display Value
        /// </summary>
        public int ChatDisplayValue { get; set; }

        /// <summary>
        /// Chat Display Duration
        /// </summary>
        public string ChatDisplayDuration { get; set; }

        /// <summary>
        /// Chat Char Limit
        /// </summary>
        public int ChatCharLimit { get; set; }

      

        /// <summary>
        ///Is Message Enabled
        /// </summary>
        public bool Message { get; set; }

        /// <summary>
        ///Is Card Enabled
        /// </summary>
        public bool Card { get; set; }

        /// <summary>
        ///Is RecommendedList Enabled
        /// </summary>
        public bool RecommendedList { get; set; }

        /// <summary>
        ///Is ScheduleVisit Enabled
        /// </summary>
        public bool ScheduleVisit { get; set; }

        /// <summary>
        ///Is PaymentLink Enabled
        /// </summary>
        public bool PaymentLink { get; set; }

        /// <summary>
        ///Is CustomerProfile and CustomerProduct Enabled
        /// </summary>
        public bool ChatProfileProduct { get; set; }




        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Modified By
        /// </summary>
        public int ModifiedBy { get; set; }

        /// <summary>
        /// Modified Date
        /// </summary>
        public string ModifiedDate { get; set; }
    }


}
