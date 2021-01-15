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

    public class EnumModel
    {
        public int Value { get; set; }
        public string Name { get; set; }
    }

    public class RegionZoneList
    {
        public List<RegionMaster> regionMasters { get; set; }
        public List<EnumModel> enumModels { get; set; }
    }
}
