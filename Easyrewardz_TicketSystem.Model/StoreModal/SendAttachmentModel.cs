using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
    public class SendAttachmentModel
    {
        public string programCode { get; set; }
        public string mobileNumber { get; set; }
        public string mediaBinaryData { get; set; }
        public string mediaType { get; set; }
        public string mediaName { get; set; }
        public string additionalDetails { get; set; }
        public string fileExtention { get; set; }
        public string textMessage { get; set; }
        public string source { get; set; }
    }

    public class SendAttachmentResponse
    {
        public string isRequestQueued { get; set; }
        public string mediaURL { get; set; }
        public string source { get; set; }
    }

}
