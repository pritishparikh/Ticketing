using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class DashBoardGraphModel
    {
        public int OpenPriorityTicketCount { get; set; }
        public List<OpenByPriorityModel> PriorityChart { get; set; }
        public List<TicketToBillGraphModel> tickettoBillGraph { get; set; }
        public List<TicketSourceModel> ticketSourceGraph { get; set; }

        public List<TicketToTask> tickettoTaskGraph { get; set; }
        public List<TicketToClaim> tickettoClaimGraph { get; set; }
    }

        public class OpenByPriorityModel
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

    public class TicketToBillGraphModel
    {
        public int ticketSourceID { set; get; }
        public string ticketSourceName { set; get; }
      
        public int totalBills { set; get; }
        public int ticketedBills { set; get; }

    }

    public class TicketToTask
    {
        public int totalTickets { set; get; }
        public int taskTickets { set; get; }
        public string Day { set; get; }
    }

    public class TicketToClaim
    {
        public int totalTickets { set; get; }
        public int ClaimTickets { set; get; }
        public string Day { set; get; }
    }

}
