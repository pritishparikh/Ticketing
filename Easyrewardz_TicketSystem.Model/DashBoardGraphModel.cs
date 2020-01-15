using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class DashBoardGraphModel
    {
        public List<PriorityGraphModel> PriorityChart { get; set; }
        public List<TicketSourceModel> ticketSourceGraph { get; set; }

    }

    public class PriorityGraphModel
    {
        public int priorityID { set; get; }
        public string priorityName { set; get; }
        public int priorityCount { set; get; }
    }

    public class TicketSourceModel
    {
        public int ticketSourceID { set; get; }
        public string ticketSourceName { set; get; }
        public int ticketSourceCount { set; get; }
    }
}
