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
        /// IsCustomerChating
        /// </summary>
        public bool IsCustomerChating { get; set; }
        /// <summary>
        /// CustomerChatingID
        /// </summary>
        public int CustomerChatingID { get; set; }
        /// <summary>
        /// HSCampaignResponseList
        /// </summary>
        public List<HSCampaignResponse> HSCampaignResponseList { get; set; }
    }

    public class CampaingCustomerFilterRequest
    {
        /// <summary>
        /// Campaign Script ID
        /// </summary>
        public int CampaignScriptID { get; set; }

        /// <summary>
        /// Page No
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Filter Status
        /// </summary>
        public string FilterStatus { get; set; } = "All";

        /// <summary>
        /// Mobile Number
        /// </summary>
        public string MobileNumber { get; set; } = "";
    }

    public class CampaignCustomerDetails
    {
        /// <summary>
        /// Campaign Customer Model
        /// </summary>
        public List<CampaignCustomerModel> CampaignCustomerModel { get; set; }

        /// <summary>
        /// Campaign Customer Count
        /// </summary>
        public int CampaignCustomerCount { get; set; }
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

    public class SendFreeTextRequest
    {
        /// <summary>
        /// To
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// ProgramCode
        /// </summary>
        public string ProgramCode { get; set; }
        /// <summary>
        /// TemplateName
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// TemplateNamespace
        /// </summary>
        //public string TemplateNamespace { get; set; }
        /// <summary>
        /// TemplateName
        /// </summary>
        public List<string> AdditionalInfo { get; set; }
        /// <summary>
        /// language
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// whatsAppNumber
        /// </summary>
        public string whatsAppNumber { get; set; }
    }

    public class GetWhatsappMessageDetailsModal
    {
       
        /// <summary>
        /// ProgramCode
        /// </summary>
        public string ProgramCode { get; set; }
        
    }

    public class GetWhatsappMessageDetailsResponse
    {

        /// <summary>
        /// ProgramCode
        /// </summary>
        public string ProgramCode { get; set; }
        /// <summary>
        /// TemplateName
        /// </summary>
        public string TemplateName { get; set; } = "";
        /// <summary>
        /// TemplateNamespace
        /// </summary>
        public string TemplateNamespace { get; set; }
        /// <summary>
        /// TemplateText
        /// </summary>
        public string TemplateText { get; set; }
        /// <summary>
        /// TemplateLanguage
        /// </summary>
        public string TemplateLanguage { get; set; }
        /// <summary>
        /// Remarks
        /// </summary>
        public string Remarks { get; set; }

    }

    public class BroadcastDetails
    {
        /// <summary>
        /// List of Campaign Execution Details Response
        /// </summary>
        public List<CampaignExecutionDetailsResponse> CampaignExecutionDetailsResponse { get; set; }

        /// <summary>
        /// Broadcast Configuration Response
        /// </summary>
        public BroadcastConfigurationResponse BroadcastConfigurationResponse { get; set; }
    }

    public class CampaignExecutionDetailsResponse
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Programcode
        /// </summary>
        public string Programcode { get; set; }
        /// <summary>
        /// StoreCode
        /// </summary>
        public string StoreCode { get; set; }
        /// <summary>
        /// CampaignCode
        /// </summary>
        public string CampaignCode { get; set; }
        /// <summary>
        /// ChannelType
        /// </summary>
        public string ChannelType { get; set; }
        /// <summary>
        /// ExecutionDate
        /// </summary>
        public string ExecutionDate { get; set; }
    }

    public class BroadcastConfigurationResponse
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// SmsFlag
        /// </summary>
        public bool SmsFlag { get; set; }
        /// <summary>
        /// EmailFlag
        /// </summary>
        public bool EmailFlag { get; set; }
        /// <summary>
        /// WhatsappFlag
        /// </summary>
        public bool WhatsappFlag { get; set; }
        /// <summary>
        /// MaxClickAllowed
        /// </summary>
        public int MaxClickAllowed { get; set; }
        /// <summary>
        /// SMSClickCount
        /// </summary>
        public int SMSClickCount { get; set; }
        /// <summary>
        /// EmailClickCount
        /// </summary>
        public int EmailClickCount { get; set; }
        /// <summary>
        /// WhatsappClickCount
        /// </summary>
        public int WhatsappClickCount { get; set; }
        /// <summary>
        /// HideSMS
        /// </summary>
        public bool DisableSMS { get; set; }
        /// <summary>
        /// HideEmail
        /// </summary>
        public bool DisableEmail { get; set; }
        /// <summary>
        /// HideWhatsapp
        /// </summary>
        public bool DisableWhatsapp { get; set; }
    }

    public class CampaingURLList
    {
        /// <summary>
        /// Getwhatsappmessagedetails
        /// </summary>
        public string Getwhatsappmessagedetails { get; set; }
        /// <summary>
        /// Sendcampaign
        /// </summary>
        public string Sendcampaign { get; set; }
        /// <summary>
        /// Sendsms
        /// </summary>
        public string Sendsms { get; set; }
        /// <summary>
        /// GetUserATVDetails
        /// </summary>
        public string GetUserATVDetails { get; set; }
        /// <summary>
        /// GetKeyInsight
        /// </summary>
        public string GetKeyInsight { get; set; }
        /// <summary>
        /// GetLastTransactionDetails
        /// </summary>
        public string GetLastTransactionDetails { get; set; }
        /// <summary>
        /// MakeBellActive
        /// </summary>
        public string MakeBellActive { get; set; }
    }
}
