using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class TicketNotes
    {
        public int TicketNoteID { get; set; }
        public int TicketID { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }

    }
}
