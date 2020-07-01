using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class AgentRecentChatHistory
    {
        /// <summary>
        /// Chat ID
        /// </summary>
        public int ChatID { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Store Manager ID
        /// </summary>
        public int StoreManagerID { get; set; }

        /// <summary>
        /// Agent Name
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// Customer Mobile
        /// </summary>
        public string CustomerMobile { get; set; }

        /// <summary>
        /// Chat Count
        /// </summary>
        public int ChatCount { get; set; }

        /// <summary>
        /// Time Ago
        /// </summary>
        public string TimeAgo { get; set; }

        /// <summary>
        /// Chat Status
        /// </summary>
        public string ChatStatus { get; set; }

    }

    public class AgentCustomerChatHistory
    {
        /// <summary>
        /// Chat ID
        /// </summary>
        public int ChatID { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Store Manager ID
        /// </summary>
        public int StoreManagerID { get; set; }

        /// <summary>
        /// Agent Name
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Mobile
        /// </summary>
        public string CustomerMobile { get; set; }

        /// <summary>
        /// Chat Count
        /// </summary>
        public int ChatCount { get; set; }

        /// <summary>
        /// Time Ago
        /// </summary>
        public string TimeAgo { get; set; }

        /// <summary>
        /// Chat Status
        /// </summary>
        public string ChatStatus { get; set; }

    }

}
