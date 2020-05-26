using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class CustomerChatMaster
    {
        /// <summary>
        /// Chat ID of Customer
        /// </summary>
        public int ChatID { get; set; }

        /// <summary>
        /// Store ID 
        /// </summary>
        public string StoreID { get; set; }

        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Customer ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Customer Name 
        /// </summary>
        public string CumtomerName { get; set; }

        /// <summary>
        /// Customer Mobile No 
        /// </summary>
        public string MobileNo { get; set; }

        /// <summary>
        /// New Message Count
        /// </summary>
        public int MessageCount { get; set; }

        /// <summary>
        /// Time Ago Chat
        /// </summary>
        public string TimeAgo { get; set; }

        /// <summary>
        /// On going Chat Count
        /// </summary>
        public int OngoingChatCount { get; set; }

        /// <summary>
        /// Chat Status 
        /// </summary>
        public int ChatStatus { get; set; }

        /// <summary>
        /// Created BY
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created date 
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Updated Date
        /// </summary>
        public DateTime UpdatedDate{ get; set; }

        /// <summary>
        /// StoreManagerId 
        /// </summary>
        public int StoreManagerId { get; set; }

        /// <summary>
        /// StoreManager Name
        /// </summary>
        public string StoreManagerName { get; set; }
    }

    public class CustomerChatMessages
    {
        /// <summary>
        /// Chat ID of Customer
        /// </summary>
        public int ChatID { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Attachment
        /// </summary>
        public string Attachment { get; set; }

        /// <summary>
        /// is by customer or not
        /// </summary>
        public bool ByCustomer { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Status of the chat
        /// </summary>
        public string ChatStatus { get; set; }

        /// <summary>
        /// StoreManagerId
        /// </summary>
        public int StoreManagerId { get; set; }

        /// <summary>
        /// Created BY
        /// </summary>
        public int CreatedBy { get; set; }


        /// <summary>
        /// User name who Created
        /// </summary>
        public string CreateByName { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        public int ModifyBy { get; set; }

        /// <summary>
        /// Updated Date
        /// </summary>
        public string ModifyByName { get; set; }

        /// <summary>
        /// Date when chat happend
        /// </summary>
        public string ChatDate { get; set; }

        /// <summary>
        /// time of Chat
        /// </summary>
        public string ChatTime { get; set; }

        /// <summary>
        /// Agent ProfilePic Path
        /// </summary>
        public string AgentProfilePic { get; set; }


        /// <summary>
        /// Customer ProfilePic Path
        /// </summary>
        public string CustomerProfilePic { get; set; }

        /// <summary>
        /// IsBotReply
        /// </summary>
        /// 
        public bool IsBotReply { get; set; }

    }


    public class CustomerChatModel
    {
        /// <summary>
        /// Chat ID of Customer
        /// </summary>
        public int ChatID { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Attachment
        /// </summary>
        public string Attachment { get; set; }

        /// <summary>
        /// is by customer or not
        /// </summary>
        public bool ByCustomer { get; set; }


        /// <summary>
        /// Status of the chat
        /// </summary>
        public int ChatStatus { get; set; }

        /// <summary>
        /// StoreManagerId
        /// </summary>
        public int StoreManagerId { get; set; }

        /// <summary>
        /// Created BY
        /// </summary>
        public int CreatedBy { get; set; }


    }

}
