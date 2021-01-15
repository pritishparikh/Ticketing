using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ChatCardImageUploadModel
    {
        /// <summary>
        /// Image Upload Log ID
        /// </summary>
        public int ImageUploadLogID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Item ID
        /// </summary>
        public string ItemID { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// Store ID
        /// </summary>
        public int StoreID { get; set; }

        /// <summary>
        /// Store Code
        /// </summary>
        public string StoreCode { get; set; }

        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Store Address
        /// </summary>
        public string StoreAddress { get; set; }

        /// <summary>
        /// IsAddedToLibrary
        /// </summary>
        public bool IsAddedToLibrary { get; set; }
       
        /// <summary>
        /// RejectionReason
        /// </summary>
        public string RejectionReason { get; set; }
        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Modify By
        /// </summary>
        public int ModifyBy { get; set; }

        /// <summary>
        /// Modify By Name
        /// </summary>
        public string ModifyByName { get; set; }

        /// <summary>
        /// Modify Date
        /// </summary>
        public string ModifyDate { get; set; }
    }

    public class ChatCardConfigurationModel 
    {
        /// <summary>
        /// Card Item ID
        /// </summary>
        public int CardItemID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Card Item
        /// </summary>
        public string CardItem { get; set; }

        /// <summary>
        /// Is Enabled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Modify By
        /// </summary>
        public int ModifyBy { get; set; }

        /// <summary>
        /// Modify By Name
        /// </summary>
        public string ModifyByName { get; set; }

        /// <summary>
        /// Modify Date
        /// </summary>
        public string ModifyDate { get; set; }
    }

    public class CardImageApprovalModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Tenant ID
        /// </summary>
        public int TenantID { get; set; }

        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Approval Type
        /// </summary>
        public string ApprovalType { get; set; }

        /// <summary>
        /// Is Enabled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Created By Name
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Modify By
        /// </summary>
        public int ModifyBy { get; set; }

        /// <summary>
        /// Modify By Name
        /// </summary>
        public string ModifyByName { get; set; }

        /// <summary>
        /// Modify Date
        /// </summary>
        public string ModifyDate { get; set; }


    }

}
