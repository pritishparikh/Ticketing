using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class AgentRecentChatHistory
    {
        public int ChatID { get; set; }
        public string Message { get; set; }
        public int StoreManagerID { get; set; }
        public string AgentName { get; set; }
        public int ChatCount { get; set; }
        public string TimeAgo { get; set; }
    
    }

    public class AgentCustomerChatHistory
    {
        public int ChatID { get; set; }
        public string Message { get; set; }
        public int StoreManagerID { get; set; }
        public string AgentName { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int ChatCount { get; set; }
        public string TimeAgo { get; set; }

    }

}
