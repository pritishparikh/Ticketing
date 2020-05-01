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
}
