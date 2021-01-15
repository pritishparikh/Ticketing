using System;
using System.Collections.Generic;
using System.Text;

namespace Easyrewardz_TicketSystem.Model.StoreModal
{
   public class SlotFilterModel
    {

        public List<SlotStoreFilter> SlotStores { get; set; }
        public List<OperationalDaysFilter> OperationalDays { get; set; }
        public List<SlotTemplateModel> SlotTemplate { get; set; }
    }

    public class OperationalDaysFilter
    {
        public string DayIDs { get; set; }

        public string DayNames { get; set; }
    }


    public class SlotStoreFilter
    {
        public int StoreID { get; set; }

        public string StoreCode { get; set; }
    }


}
