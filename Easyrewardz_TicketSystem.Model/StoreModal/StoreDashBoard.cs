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




    public class StoreDashboardClaimModel
    {
        public int claimID { get; set; }
        public int ticketID { get; set; }
        public int claimissueType { get; set; }
        public int ticketMapped { get; set; }
        public int claimsubcat { get; set; }
        public int assignTo { get; set; }
        public int claimcat { get; set; }
        public int claimraise { get; set; }
        public int taskID { get; set; }
        public int claimstatus { get; set; }
        public int taskmapped { get; set; }
        public int raisedby { get; set; }
    }

    public class StoreDashboardClaimResponseModel
    {

        public string claimID { get; set; }
        public int ticketID { get; set; }
        public string claimissueType { get; set; }
        public int ticketMapped { get; set; }
        public string claimsubcat { get; set; }
        public string claimcat { get; set; }
        public int claimraise { get; set; }
        public int taskID { get; set; }
        public string claimstatus { get; set; }
        public int taskmapped { get; set; }
        public int raisedby { get; set; }
        public string CreatedOn { get; set; }
        public string AssigntoId { get; set; }
        public string createdID { get; set; }
    }



}
