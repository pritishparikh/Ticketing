using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class FileUploadLogs
    {

        public int FileUploadLogsID { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string Date { get; set; }
        public string FileUploadStatus { get; set; }

        public string SuccessFilePath { get; set; }
        public string ErrorFilePath { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        
    }
}
