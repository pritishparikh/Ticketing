using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
   public class CustomerChatSuggestionModel
    {
        /// <summary>
        ///SuggestionID
        /// </summary>
        public int SuggestionID { get; set; }

        /// <summary>
        /// SuggestionText
        /// </summary>
        public string SuggestionText { get; set; }


        /// <summary>
        ///TagID
        /// </summary>
        public int TagID { get; set; }


        /// <summary>
        ///TagName
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// CreatedBy
        /// </summary>
        public int CreatedBy { get; set; } 

        /// <summary>
        /// CreatedDate
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// ModifyBy
        /// </summary>
        public int ModifyBy { get; set; }

        /// <summary>
        /// ModifiedDate
        /// </summary>
        public string ModifiedDate { get; set; }
    }


    public class ChatSuggestionTags
    {
        /// <summary>
        ///TagID
        /// </summary>
        public int TagID { get; set; }


        /// <summary>
        ///TagName
        /// </summary>
        public string TagName { get; set; }
    }
}
