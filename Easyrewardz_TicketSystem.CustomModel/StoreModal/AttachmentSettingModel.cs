using System.Collections.Generic;

namespace Easyrewardz_TicketSystem.CustomModel
{
    public class AttachmentSettingResponseModel
    {
        public List<StoreAttachmentFileFormat> StoreAttachmentFileFormatList { get; set; }
        public List<ArrachementSize> ArrachementSizeList { get; set; }
        public List<AttachmentSettings> AttachmentSettingsList { get; set; }
    }

    public class StoreAttachmentFileFormat
    {
        public int FormatID { get; set; }
        public string FileFormaName { get; set; }
    }

    public class ArrachementSize
    {
        public int Numb { get; set; }
        public string NumbMB { get; set; }
    }

    public class AttachmentSettings
    {
        public int SettingID { get; set; }
        public int AttachmentSize { get; set; }
        public int FileFomatID { get; set; }
        public int CreatedBy { get; set; }
    }

    public class AttachmentSettingsRequest
    {
        public int AttachmentSize { get; set; }
        public string FileFomatID { get; set; }
        public int CreatedBy { get; set; }
    }
}
