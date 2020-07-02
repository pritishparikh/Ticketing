using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class ChatSoundNotificationModel
    {
        /// <summary>
        ///  ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantID { get; set; }


        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }


        /// <summary>
        /// New Chat Sound ID
        /// </summary>
        public int NewChatSoundID { get; set; }


        /// <summary>
        /// New Chat Sound File
        /// </summary>
        public string NewChatSoundFile { get; set; }

        /// <summary>
        ///New Chat Sound Volume
        /// </summary>
        public int NewChatSoundVolume { get; set; }


        /// <summary>
        /// New Message Sound ID
        /// </summary>
        public int NewMessageSoundID { get; set; }


        /// <summary>
        /// New Message Sound File
        /// </summary>
        public string NewMessageSoundFile { get; set; }


        /// <summary>
        /// New Message Sound Volume
        /// </summary>
        public int NewMessageSoundVolume { get; set; }


        /// <summary>
        ///Is New Chat Notification Enabled
        /// </summary>
        public bool IsNotiNewChat { get; set; }

        /// <summary>
        ///Is New Message Notification Enabled
        /// </summary>
        public bool IsNotiNewMessage { get; set; }


        /// <summary>
        ///Is Set to Default Sound Setting
        /// </summary>
        public bool IsDefault { get; set; }


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


    public class ChatSoundModel
    {
        /// <summary>
        ///  Sound ID
        /// </summary>
        public int SoundID { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public int TenantID { get; set; }


        /// <summary>
        /// Program Code
        /// </summary>
        public string ProgramCode { get; set; }


        /// <summary>
        /// Sound File Name
        /// </summary>
        public string SoundFileName { get; set; }

        /// <summary>
        /// Sound File Url
        /// </summary>
        public string SoundFileUrl { get; set; }


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
