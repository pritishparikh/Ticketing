using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{

    public class StoreDashboardModel
    {
        public int taskid { get; set; }
        public int Department { get; set; }      
        public string tasktitle { get; set; }
        public string taskstatus { get; set; }
        public int ticketID { get; set; }
        public int functionID { get; set; }
        public string CreatedOn { get; set; }
        public string AssigntoId { get; set; }
        public int createdID { get; set; }
        public string taskwithTicket { get; set; }
        public string taskwithClaim { get; set; }
        public int claimID { get; set; }
        public string Priority { get; set; }
    }

    public class StoreDashboardResponseModel
    {
        public int taskid { get; set; }
        public string Department { get; set; }
        public string storeName { get; set; }
        public string StoreAddress { get; set; }
        public string tasktitle { get; set; }
        public string taskstatus { get; set; }
        public int ticketID { get; set; }
        public int functionID { get; set; }
        public string CreatedOn { get; set; }
        public string AssigntoId { get; set; }
        public int createdID { get; set; }
        public string taskwithTicket { get; set; }
        public string taskwithClaim { get; set; }
        public int claimID { get; set; }
        public string Priority { get; set; }
    }


}
