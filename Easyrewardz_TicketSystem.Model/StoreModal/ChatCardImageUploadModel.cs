using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ChatCardImageUploadModel
    {

        public int ImageUploadLogID { get; set; }
        public int TenantID { get; set; }
        public string ProgramCode { get; set; }
        public string ItemID { get; set; }
        public string ImageURL { get; set; }
        public int StoreID { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public bool IsAddedToLibrary { get; set; }

        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyByName { get; set; }
        public string ModifyDate { get; set; }
    }

    public class ChatCardConfigurationModel 
    {
        public int CardItemID { get; set; }
        public int TenantID { get; set; }
        public string ProgramCode { get; set; }
        public string CardItem { get; set; }
        public bool IsEnabled { get; set; }

        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public string ModifyByName { get; set; }
        public string ModifyDate { get; set; }
    }

}
