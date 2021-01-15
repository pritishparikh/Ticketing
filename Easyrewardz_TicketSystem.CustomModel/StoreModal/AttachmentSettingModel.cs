using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class AttachmentSettingResponseModel
    {
        /// <summary>
        /// Store Attachment File Format List
        /// </summary>
        public List<StoreAttachmentFileFormat> StoreAttachmentFileFormatList { get; set; }

        /// <summary>
        /// Arrachement Size List
        /// </summary>
        public List<ArrachementSize> ArrachementSizeList { get; set; }

        /// <summary>
        /// Attachment Settings List
        /// </summary>
        public List<AttachmentSettings> AttachmentSettingsList { get; set; }
    }

    public class StoreAttachmentFileFormat
    {
        /// <summary>
        /// Format ID
        /// </summary>
        public int FormatID { get; set; }

        /// <summary>
        /// File Format Name
        /// </summary>
        public string FileFormaName { get; set; }
    }

    public class ArrachementSize
    {
        /// <summary>
        /// Numb
        /// </summary>
        public int Numb { get; set; }

        /// <summary>
        /// NumbMB
        /// </summary>
        public string NumbMB { get; set; }
    }

    public class AttachmentSettings
    {
        /// <summary>
        /// Setting ID
        /// </summary>
        public int SettingID { get; set; }

        /// <summary>
        /// Attachment Size
        /// </summary>
        public int AttachmentSize { get; set; }

        /// <summary>
        /// File Format ID
        /// </summary>
        public int FileFomatID { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }
    }

    public class AttachmentSettingsRequest
    {
        /// <summary>
        /// Attachment Size
        /// </summary>
        public int AttachmentSize { get; set; }

        /// <summary>
        /// File Format ID
        /// </summary>
        public string FileFomatID { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        public int CreatedBy { get; set; }
    }
}
