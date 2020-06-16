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
        const int maxPageSize = 20;

        public int ChatId { get; set; }

        public int pageNumber { get; set; } = 1;

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
        public string ProgramCode { get; set; }
        public int TenantID { get; set; }
        public int ChatSessionValue { get; set; }
        public string ChatSessionDuration { get; set; }
        public int ChatDisplayValue { get; set; }
        public string ChatDisplayDuration { get; set; }
        public int ChatCharLimit { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }


}
