﻿using System;
using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.Model
{
    public class CampaignCustomerModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// CampaignScriptID
        /// </summary>
        public int CampaignScriptID { get; set; }
        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// CustomerNumber
        /// </summary>
        public string CustomerNumber { get; set; }
        /// <summary>
        /// CustomerEmailID
        /// </summary>
        public string CustomerEmailID { get; set; }
        /// <summary>
        /// DOB
        /// </summary>
        public string DOB { get; set; }
        /// <summary>
        /// CampaignDate
        /// </summary>
        public string CampaignDate { get; set; }
        /// <summary>
        /// ResponseID
        /// </summary>
        public int ResponseID { get; set; }
        /// <summary>
        /// CallRescheduledTo
        /// </summary>
        public string CallRescheduledTo { get; set; }
        /// <summary>
        /// DoesTicketRaised
        /// </summary>
        public int DoesTicketRaised { get; set; }
        /// <summary>
        /// StatusName
        /// </summary>
        public string StatusName { get; set; }
        /// <summary>
        /// StatusID
        /// </summary>
        public int StatusID { get; set; }
        /// <summary>
        /// StatusID
        /// </summary>
        public string Programcode { get; set; }
        /// <summary>
        /// StatusID
        /// </summary>
        public string Storecode { get; set; }
        /// <summary>
        /// StatusID
        /// </summary>
        public int StoreManagerId { get; set; }

        /// <summary>
        /// SmsFlag
        /// </summary>
        public bool SmsFlag { get; set; }
        /// <summary>
        /// EmailFlag
        /// </summary>
        public bool EmailFlag { get; set; }
        /// <summary>
        /// MessengerFlag
        /// </summary>
        public bool MessengerFlag { get; set; }
        /// <summary>
        /// BotFlag
        /// </summary>
        public bool BotFlag { get; set; }

        /// <summary>
        /// HSCampaignResponseList
        /// </summary>
        public List<HSCampaignResponse> HSCampaignResponseList { get; set; }
    }

    public class HSCampaignResponse
    {
        /// <summary>
        /// ResponseID
        /// </summary>
        public int ResponseID { get; set; }
        /// <summary>
        /// Response
        /// </summary>
        public string Response { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// StatusName
        /// </summary>
        public string StatusName { get; set; }
    }

    public class CampaignResponseInput
    {
        /// <summary>
        /// Campaign Customer ID
        /// </summary>
        public int CampaignCustomerID { get; set; }
        /// <summary>
        /// Response ID
        /// </summary>
        public int ResponseID { get; set; }
        /// <summary>
        /// Call ReScheduled To
        /// </summary>
        public string CallReScheduledTo { get; set; }
        /// <summary>
        /// Call ReScheduled To Date
        /// </summary>
        public DateTime? CallReScheduledToDate { get; set; }
    }

    public class ShareChatbotModel
    {
        /// <summary>
        /// StoreID
        /// </summary>
        public string StoreID { get; set; }
        /// <summary>
        /// ProgramCode
        /// </summary>
        public string ProgramCode { get; set; }
        /// <summary>
        /// CustomerID
        /// </summary>
        public string CustomerID { get; set; }
        /// <summary>
        /// CustomerMobileNumber
        /// </summary>
        public string CustomerMobileNumber { get; set; }
        /// <summary>
        /// StoreManagerId
        /// </summary>
        public int StoreManagerId { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public int CampaignScriptID { get; set; }
    }

    public class ChatSendSMS
    {
        /// <summary>
        /// MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// SenderId
        /// </summary>
        public string SenderId { get; set; }
        /// <summary>
        /// SmsText
        /// </summary>
        public string SmsText { get; set; }
    }

    public class ChatSendSMSResponse
    {
        /// <summary>
        /// Guid
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// SubmitDate
        /// </summary>
        public string SubmitDate { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ErrorSEQ
        /// </summary>
        public string ErrorSEQ { get; set; }
        /// <summary>
        /// ErrorCODE
        /// </summary>
        public string ErrorCODE { get; set; }
    }
}