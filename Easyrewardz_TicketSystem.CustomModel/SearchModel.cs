﻿using System;
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

        public int? SLA_DueON { get; set; }

        public int? Ticket_StatusID { get; set; }

    }

    public class SearchDataByCustomerType
    {

        public string CustomerMobileNo { get; set; }

        public string CustomerEmailID { get; set; }

        public int? TicketID { get; set; }
      
        public int? TicketStatusID { get; set; }

    }

    public class SearchDataByTicketType
    {

        public int? TicketPriorityID { get; set; }

        public int? TicketStatusID { get; set; }

        public string ChannelOfPurchaseIds { get; set; }

        public string ActionTypes { get; set; }
        
    }

    public class SearchDataByCategoryType
    {

        public string CategoryId { get; set; }

        public string SubCategoryId { get; set; }

        public int? IssueTypeId { get; set; }

        public int? TicketStatusID { get; set; }

    }

    public class SearchDataByAll
    {
        //Column -1
        public string CreatedDate { get; set; }

        public string ModifiedDate { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public int? IssueTypeId { get; set; }

        //Column -2 
        public int? TicketSourceTypeID { get; set; }

        public string TicketIdORTitle { get; set; }

        public int? PriorityId { get; set; }

        public int? TicketSatutsID { get; set; }

        public string SLAStatus { get; set; }
        
        //Column - 3  
        public string ClaimId { get; set; }

        public string InvoiceNumberORSubOrderNo { get; set; }

        public string OrderItemId { get; set; }

        public string IsVisitStore { get; set; }

        public string IsWantVistingStore { get; set; }

        //Column - 4  
        public string CustomerEmailID { get; set; }

        public string CustomerMobileNo { get; set; }

        //public string AssignTo { get; set; }
        public int AssignTo { get; set; }

        public string StoreCodeORAddress { get; set; }

        public string WantToStoreCodeORAddress { get; set; }

        //Row - 2 and Column - 1  
        public bool HaveClaim { get; set; }

        public int? ClaimStatusId { get; set; }

        public int? ClaimCategoryId { get; set; }

        public int? ClaimSubCategoryId { get; set; }

        public int? ClaimIssueTypeId { get; set; }

        //Row - 2 and Column - 2  
        public bool HaveTask { get; set; }

        public int? TaskStatusId { get; set; }

        public int? TaskDepartment_Id { get; set; }

        public int? TaskFunction_Id { get; set; }

    }
}
