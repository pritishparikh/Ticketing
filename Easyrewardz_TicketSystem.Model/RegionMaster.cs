using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model
{
    public class RegionMaster
    {
        public int RegionID { get; set; }
        public int PinCodeID { get; set; }
        public string RegionName { get; set; }
    }

    public class RegionZoneMaster
    {
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public int ZoneID { get; set; }
        public string ZoneName { get; set; }
    }
}
