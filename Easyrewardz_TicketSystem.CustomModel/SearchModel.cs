using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class SearchModel
    {
        public int TenantID { get; set; }

        public int HeaderStatusId { get; set; }

        public int AssigntoId { get; set; }

        public int ActiveTabId { get; set; }

        /*Model for the each tab */
        public SearchDataByDate searchDataByDate { get; set; }

        public SearchDataByCustomerType searchDataByCustomerType { get; set; }

        public SearchDataByTicketType searchDataByTicketType { get; set; }

        public SearchDataByCategoryType searchDataByCategoryType { get; set; }

        public SearchDataByAll searchDataByAll { get; set; }
    }


    public class SearchDataByDate
    {

        public string Ticket_CreatedOn { get; set; }

        public string Ticket_ModifiedOn { get; set; }

        public int SLA_DueON { get; set; }

        public int Ticket_StatusID { get; set; }

    }

    public class SearchDataByCustomerType
    {

        public string Ticket_CreatedOn { get; set; }

        public string Ticket_ModifiedOn { get; set; }

        public int SLA_DueON { get; set; }

        public int Ticket_StatusID { get; set; }

    }

    public class SearchDataByTicketType
    {

        public string Ticket_CreatedOn { get; set; }

        public string Ticket_ModifiedOn { get; set; }

        public int SLA_DueON { get; set; }

        public int Ticket_StatusID { get; set; }

    }

    public class SearchDataByCategoryType
    {

        public string Ticket_CreatedOn { get; set; }

        public string Ticket_ModifiedOn { get; set; }

        public int SLA_DueON { get; set; }

        public int Ticket_StatusID { get; set; }

    }

    public class SearchDataByAll
    {

        public string Ticket_CreatedOn { get; set; }

        public string Ticket_ModifiedOn { get; set; }

        public int SLA_DueON { get; set; }

        public int Ticket_StatusID { get; set; }

    }
}
