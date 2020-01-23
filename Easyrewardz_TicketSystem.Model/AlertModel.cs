using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class AlertModel
    {

        public int AlertID { get; set; }
        public string RoleName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string isAlertActive { get; set; }

        public CommunicationMode ModeOfCommunication { get; set; }
    }

    public class CommunicationMode
    {
        public bool isByEmail { get; set; }
        public bool isBySMS { get; set; }
        public bool isByNotification { get; set; }
    }


}
